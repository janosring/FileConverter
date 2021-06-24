using FileConverter.Core.Converters;
using System;
using System.ComponentModel;

namespace FileConverter.Core
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly JsonConverter _jsonConverter;
        private readonly CsvConverter _csvConverter;
        private readonly XmlConverter _xmlConverter;

        public ConverterFactory(JsonConverter jsonConverter, CsvConverter csvConverter, XmlConverter xmlConverter)
        {
            _jsonConverter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
            _csvConverter = csvConverter ?? throw new ArgumentNullException(nameof(csvConverter));
            _xmlConverter = xmlConverter ?? throw new ArgumentNullException(nameof(xmlConverter));
        }

        public IConverter GetConverter(Format format)
        {
            switch (format)
            {
                case Format.Csv:
                    return _csvConverter;
                case Format.Json:
                    return _jsonConverter;
                case Format.Xml:
                    return _xmlConverter;
                default:
                    throw new InvalidEnumArgumentException();
            }

        }
    }
}
