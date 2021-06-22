using System.Collections.Generic;

namespace FileConverter.Core.Converters
{
    public interface IConverter
    {
        bool Validate(string source);
        Dictionary<string, object> ConvertToIntermediateModel(string source);
        string ConvertFromIntermediateModel(Dictionary<string, object> source);
        
    }
}