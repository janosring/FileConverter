using FileConverter.Core;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.ConsoleApp
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices() 
        {
            var serviceCollection = new ServiceCollection();


            return serviceCollection;
        }
    }
}
