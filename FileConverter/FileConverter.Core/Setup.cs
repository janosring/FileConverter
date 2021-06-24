using FileConverter.Core.Converters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FileConverter.Core
{
    public static class Setup
    {
        public static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<CsvConverter>();
            serviceCollection.AddSingleton<JsonConverter>();
            serviceCollection.AddSingleton<XmlConverter>();

            serviceCollection.AddSingleton<ICollection<IConverter>>(serviceProvider => new List<IConverter> 
                {
                    serviceProvider.GetService<CsvConverter>(),
                    serviceProvider.GetService<JsonConverter>(),
                    serviceProvider.GetService<XmlConverter>()
                }
            );

            serviceCollection.AddSingleton<IConverterFactory, ConverterFactory>();


            serviceCollection.AddSingleton<IFileConverter, FileConverter>();

            return serviceCollection;
        }
    }
}
