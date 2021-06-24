using FileConverter.Core.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Core
{
    public class FileConverter : IFileConverter
    {
        private readonly IConverterFactory _converterFactory;
        private readonly ICollection<IConverter> _converters;

        public FileConverter(IConverterFactory converterFactory, ICollection<IConverter> converters)
        {
            _converterFactory = converterFactory ?? throw new ArgumentNullException(nameof(converterFactory));
            _converters = converters ?? throw new ArgumentNullException(nameof(converters));
        }

        public string Convert(string source, Format targetFormat) 
        {
            var sourceConverter = _converters.FirstOrDefault(coverter => coverter.Validate(source));

            if (sourceConverter == null) throw new ArgumentException($"Not supported source format: {source}");

            var intermediateModel = sourceConverter.ConvertToIntermediateModel(source);

            var targetConverter = _converterFactory.GetConverter(targetFormat);
            var target = targetConverter.ConvertFromIntermediateModel(intermediateModel);

            return target;
        }
    }
}
