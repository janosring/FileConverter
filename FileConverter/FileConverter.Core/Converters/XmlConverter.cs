using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileConverter.Core.Converters
{
    public class XmlConverter : IConverter
    {
        public string ConvertFromIntermediateModel(Dictionary<string, object> source)
        {
            var stingBuilder = new StringBuilder();
            stingBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            stingBuilder.Append(Environment.NewLine);
            stingBuilder.Append("<root>"); 
            stingBuilder.Append(Environment.NewLine);

            ConvertNode(source);

            stingBuilder.Append("</root>");
            return stingBuilder.ToString();

            void ConvertNode(Dictionary<string, object> node) 
            {
                foreach (var propertyNameValue in node)
                {
                    stingBuilder.Append($"<{propertyNameValue.Key}>");

                    if (propertyNameValue.Value is string)
                    {
                        stingBuilder.Append(propertyNameValue.Value);
                    }
                    else
                    {
                        stingBuilder.Append(Environment.NewLine);
                        ConvertNode((Dictionary<string, object>)propertyNameValue.Value);
                    }

                    stingBuilder.Append($"</{propertyNameValue.Key}>");
                    stingBuilder.Append(Environment.NewLine);
                }
            }
        }

        public Dictionary<string, object> ConvertToIntermediateModel(string source)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string source) =>
            source.Contains("{") &&
            source.Contains("}") &&
            source.Count(c => c == '{') == source.Count(c => c == '}');
    }        
}
