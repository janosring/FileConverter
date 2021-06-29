using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileConverter.Core.Converters
{
    public class CsvConverter : IConverter
    {
        public bool Validate(string source) =>
            source.Contains(",") && Regex.Split(source, Environment.NewLine).Length == 2 &&
            Regex.Split(source, Environment.NewLine)[0].Split(',').Length == Regex.Split(source, Environment.NewLine)[1].Split(',').Length
            ;

        public Dictionary<string, object> ConvertToIntermediateModel(string source)
        {
            var model = new Dictionary<string, object>();

            var properties = Regex.Split(source, Environment.NewLine)[0].Split(',');
            var values = Regex.Split(source, Environment.NewLine)[1].Split(',');

            for (int i = 0; i < properties.Length; i++)
            {
                AddPropertyToModel(model, properties[i], values[i]);
            }

            return model;

            void AddPropertyToModel(Dictionary<string, object> modelToAdd, string propertyName, string properyValue)
            {
                if (!propertyName.Contains("_"))
                {
                    modelToAdd.Add(propertyName, properyValue);
                    return;
                }

                var parentPropertyName = propertyName.Split('_')[0];
                var childPropertyName = propertyName.Replace($"{parentPropertyName}_", string.Empty);

                if (!modelToAdd.ContainsKey(parentPropertyName))
                {
                    modelToAdd.Add(parentPropertyName, new Dictionary<string, object>());
                }

                var childModel = modelToAdd[parentPropertyName];

                if(childModel is string)
                {
                    var value = (string)childModel;

                    var newChildModel = new Dictionary<string, object>();
                    newChildModel.Add(parentPropertyName, value);

                    AddPropertyToModel(newChildModel, childPropertyName, properyValue);
                    modelToAdd[parentPropertyName] = newChildModel;
                    return;
                }

                AddPropertyToModel((Dictionary<string, object>) childModel, childPropertyName, properyValue);
            }
        }

        public string ConvertFromIntermediateModel(Dictionary<string, object> source) 
        {
            var firstLineBuilder = new StringBuilder();
            var secondLineBuilder = new StringBuilder();

            ConvertNode(source, string.Empty);

            firstLineBuilder.Length--;
            secondLineBuilder.Length--;

            return $"{firstLineBuilder}{Environment.NewLine}{secondLineBuilder}";

            void ConvertNode(Dictionary<string, object> node, string parentNodeName)
            {
                foreach (var propertyNameValue in node)
                {
                    var propertyName = string.IsNullOrEmpty(parentNodeName)
                            ? propertyNameValue.Key
                            : $"{parentNodeName}_{propertyNameValue.Key}";

                    if (propertyNameValue.Value is string)
                    {
                        
                        firstLineBuilder.Append($"{propertyName},");
                        secondLineBuilder.Append($"{propertyNameValue.Value},");
                    }
                    else 
                    {
                        var childNode = (Dictionary<string, object>) propertyNameValue.Value;
                        ConvertNode(childNode, propertyName);
                    }
                }
            }
        }
    }
}
