using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

                    ConvertNode(new Dictionary<string, object>() { { propertyNameValue.Key, propertyNameValue.Value } });

                }
                builder.Length--;
            }
        }

        public Dictionary<string, object> ConvertToIntermediateModel(string source)
        {
            throw new NotImplementedException();
        }

    }
}
