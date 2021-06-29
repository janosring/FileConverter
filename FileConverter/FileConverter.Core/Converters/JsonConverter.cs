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

            ConvertNode(source, intermediateModel);
            return intermediateModel;

            void ConvertNode(string node, Dictionary<string, object> parent )
            {
                if (node[0] == '{')
                {
                    node = RemoveFirstCharacter(node, "{");
                    node = RemoveLastCharacter(node, "}");
                }

                var nodeNamePattern = "^\"[\\w|\\n]*\"";

                var regex = new Regex(nodeNamePattern);

                var match = regex.Match(node);
                var propertyName = match.Value;
                var restOfTheNode = node.Replace(propertyName, string.Empty);

                var properyValues = FindProperyValue(restOfTheNode);
                var rest = string.Empty;
                if (properyValues[0] != '{')
                {
                    rest = AddSimpleProperty(node, parent, propertyName, properyValues);
                }
                else
                {
                    var child = new Dictionary<string, object>();
                    ConvertNode(properyValues, child);
                    parent.Add(propertyName.Replace("\"", string.Empty), child);
                    rest = node.Replace($"{propertyName}:{properyValues}", string.Empty);
                    if (!string.IsNullOrEmpty(rest) && rest[0] == ',') rest = RemoveFirstCharacter(rest, ",");
                }

                if (!string.IsNullOrWhiteSpace(rest)) ConvertNode(rest, parent);
            }

            string FindProperyValue(string propery) 
            {
                propery = RemoveFirstCharacter(propery, ":");
                if (propery[0] != '{') 
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

            
            
        }

        private string RemoveFirstCharacter(string src, string characterToRemove)
        {
            var index = src.IndexOf(characterToRemove);
            return (index < 0)
                ? src
                : src.Remove(index, 1);
        }
        private string RemoveLastCharacter(string src, string characterToRemove)
        {
            var index = src.LastIndexOf(characterToRemove);
            return
                src.Substring(0, index > -1 ? index : src.Count());
        }

        private string AddSimpleProperty(string node, Dictionary<string, object> parent, string propertyName, string properyValues)
        {
            var properyValue = properyValues.Split(',').FirstOrDefault();

            parent.Add(propertyName.Replace("\"", string.Empty), properyValue.Replace("\"", string.Empty));
            var restOfTheNode = node.Replace($"{propertyName}:{properyValue}", string.Empty);
            if (!string.IsNullOrEmpty(restOfTheNode) && restOfTheNode[0] == ',') restOfTheNode = RemoveFirstCharacter(restOfTheNode, ",");

            return restOfTheNode;
        }
    }
}
