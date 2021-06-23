using FileConverter.Core.Converters;
using System;


namespace FileConverter.Core
{
    public class ConverterFactory
    {
        private readonly JsonConverter _jsonConverter;
        private readonly CsvConverter _csvConverter;
        private readonly XmlConverter _xmlConverter;
        public IConverter GetConverter(Format format)
        {
            throw new NotImplementedException();

        }
    }
}
