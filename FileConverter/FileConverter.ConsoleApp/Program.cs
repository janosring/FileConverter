using FileConverter.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FileConverter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Enum.TryParse(args[1], out Format targetFormat)) 
            {
                Console.WriteLine($"Wrong target format: {args[1]}");
                return;
            }

            var services = Setup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IFileConverter>().Convert(args[0], targetFormat);

        }
    }
}
