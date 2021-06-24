using FileConverter.Core;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileConverter.IntegrationTests
{
    [TestClass]
    public class FileConverterTest
    {
        [TestMethod]
        public void GIVEN_SourceIsInCSVFormat_WHEN_ConvertItToJson_THEN_TargetShouldBeTheCorrect()
        {
            //Arrange
            var source = $"name,address_line1,address_line2{Environment.NewLine}Dave,Street,Town";
            var targetFormat = Format.Json;

            var services = Setup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var converter = serviceProvider.GetService<IFileConverter>();

            //Act
            var target = converter.Convert(source, targetFormat);

            //Assert
            target.Should().Be("{\"name\":\"Dave\",\"address\":{\"line1\":\"Street\",\"line2\":\"Town\"}}");

        }
    }
}
