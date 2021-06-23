using FileConverter.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FileConverter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            if (!Enum.TryParse(args[1], out Format targetFormat)) 
            {
                Console.WriteLine($"Wrong target format: {args[1]}");
                return;
            }

            serviceProvider.GetService<IFileConverter>().Convert(args[0], targetFormat);

        }
    }
}
