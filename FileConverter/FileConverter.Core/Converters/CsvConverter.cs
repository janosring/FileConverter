using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileConverter.Core.Converters
{
    public class CsvConverter : IConverter
    {
        public bool Validate(string source) =>
            source.Contains(",") && Regex.Split(source, Environment.NewLine).Length == 2 &&
            Regex.Split(source, Environment.NewLine)[0].Split(',').Length == Regex.Split(source, Environment.NewLine)[1].Split(',').Length
            ;

        public Dictionary<string, object> ConvertToIntermediateModel(string source) => null;

        public string ConvertFromIntermediateModel(Dictionary<string, object> source) => string.Empty;
    }
}
