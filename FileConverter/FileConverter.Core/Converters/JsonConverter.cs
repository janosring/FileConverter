using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileConverter.Core.Converters
{
    public class JsonConverter : IConverter
    {
        public bool Validate(string source) => 
            source.Contains("{") && 
            source.Contains("}") && 
            source.Count(c => c == '{') == source.Count(c => c == '}');

        public string ConvertFromIntermediateModel(Dictionary<string, object> source)
        {
            var builder = new StringBuilder();
            builder.Append("{");
            ConvertNode(source);
            builder.Append("}");
            return builder.ToString();

            void ConvertNode(Dictionary<string, object> node)
            {
                foreach (var propertyNameValue in node)
                {
                    builder.Append($"\"{propertyNameValue.Key}\":");
                    if (propertyNameValue.Value is string)
                    {
                        builder.Append($"\"{propertyNameValue.Value}\",");
                        continue;
                    }

                    if (propertyNameValue.Value is Dictionary<string, object>) 
                    {
                        var nextNode = (Dictionary<string, object>)propertyNameValue.Value;
                        builder.Append("{");
                        ConvertNode(nextNode);
                        builder.Append("},");
                    }
                }
                builder.Length--;
            }
        }

        public Dictionary<string, object> ConvertToIntermediateModel(string source)
        {
            var intermediateModel = new Dictionary<string, object>();
            source = RemoveFirstCharacter(source, "{");
            source = RemoveLastCharacter(source, "}");

            ConvertNode(source);
            return intermediateModel;

            void ConvertNode(string node) 
            {                                
                var nodeNamePattern = "^\"[\\w|\\n]*\"";

                var regex = new Regex(nodeNamePattern);

                var match = regex.Match(node);
                var propertyName = match.Value;
                var restOfTheNode = node.Replace(propertyName, string.Empty);

                var properyValues = FindProperyValue(restOfTheNode);
                var properyValue = properyValues.Split(',').FirstOrDefault();


                intermediateModel.Add(propertyName.Replace("\"", string.Empty), properyValue.Replace("\"", string.Empty));
                restOfTheNode = node.Replace($"{propertyName}:{properyValue},", string.Empty);
                restOfTheNode = restOfTheNode.Replace($"{propertyName}:{properyValue}", string.Empty);

                if (!string.IsNullOrWhiteSpace(restOfTheNode)) ConvertNode(restOfTheNode);
            }           

            string FindProperyValue(string propery) 
            {
                propery = RemoveFirstCharacter(propery, ":");
                if (!propery.Contains("{")) 
                    return propery;

                var numberOfOpenBrackets = 0;
                var numberOfCloseBrackets = 0;
                var propertyValue = new StringBuilder();

                foreach (var item in propery)
                {
                    propertyValue.Append(item);

                    if (item == '{')
                        numberOfOpenBrackets++;

                    else if (item == '}')
                        numberOfCloseBrackets++;

                    if(numberOfCloseBrackets != 0 && numberOfOpenBrackets == numberOfCloseBrackets)
                    {
                        break;
                    }
                }

                return propertyValue.ToString();
            }

            string RemoveFirstCharacter(string src, string characterToRemove)
            {
                var index = src.IndexOf(characterToRemove);
                return (index < 0)
                    ? src
                    : src.Remove(index, 1);
            }
            
            string RemoveLastCharacter(string src, string characterToRemove)
            {
                var index = src.LastIndexOf(characterToRemove);
                return
                    src.Substring(0, index > -1 ? index : src.Count());
            }
        }

    }
}
