using FileConverter.Core.Converters;

namespace FileConverter.Core
{
    public interface IConverterFactory
    {
        IConverter GetConverter(Format format);
    }
}