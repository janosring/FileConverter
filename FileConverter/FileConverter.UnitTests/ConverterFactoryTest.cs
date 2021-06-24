using FileConverter.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac.Extras.Moq;
using FluentAssertions;
using FileConverter.Core.Converters;

namespace FileConverter
{
    [TestClass]
    public class ConverterFactoryTest
    {
        [TestMethod]
        public void GIVEN_CreateConverter_WHEN_TypeIsCsv_THEN_ConverterTypeShouldBeCsvConverter()
        {
            //Arrange
            using var mock = AutoMock.GetLoose();
            var factory = mock.Create<ConverterFactory>();

            //Act
            var converter = factory.GetConverter(Format.Csv);

            //Assert
            converter.Should().BeOfType<CsvConverter>();
        }
    }
}
