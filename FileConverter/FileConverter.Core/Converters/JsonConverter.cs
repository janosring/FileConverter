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
            throw new NotImplementedException();
        }

        public Dictionary<string, object> ConvertToIntermediateModel(string source)
        {
            throw new NotImplementedException();
        }

    }
}
