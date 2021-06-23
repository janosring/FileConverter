namespace FileConverter.Core
{
    public interface IFileConverter
    {
        string Convert(string source, Format targetFormat);
    }
}