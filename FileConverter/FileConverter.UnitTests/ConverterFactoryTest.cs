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

            var csvConverter = mock.Create<CsvConverter>();
            var jsonConverter = mock.Create<JsonConverter>();
            var xmlCoverter = mock.Create<XmlConverter>();

            var factory = new ConverterFactory(jsonConverter, csvConverter, xmlCoverter);

            //Act
            var converter = factory.GetConverter(Format.Csv);

            //Assert
            converter.Should().BeOfType<CsvConverter>();
        }

        [TestMethod]
        public void GIVEN_CreateConverter_WHEN_TypeIsJson_THEN_ConverterTypeShouldBeJsonConverter()
        {
            //Arrange
            using var mock = AutoMock.GetLoose();

            var csvConverter = mock.Create<CsvConverter>();
            var jsonConverter = mock.Create<JsonConverter>();
            var xmlCoverter = mock.Create<XmlConverter>();

            var factory = new ConverterFactory(jsonConverter, csvConverter, xmlCoverter);

            //Act
            var converter = factory.GetConverter(Format.Json);

            //Assert
            converter.Should().BeOfType<JsonConverter>();
        }


        [TestMethod]
        public void GIVEN_CreateConverter_WHEN_TypeIsXml_THEN_ConverterTypeShouldBeXmlConverter()
        {
            //Arrange
            using var mock = AutoMock.GetLoose();

            var csvConverter = mock.Create<CsvConverter>();
            var jsonConverter = mock.Create<JsonConverter>();
            var xmlCoverter = mock.Create<XmlConverter>();

            var factory = new ConverterFactory(jsonConverter, csvConverter, xmlCoverter);

            //Act
            var converter = factory.GetConverter(Format.Xml);

            //Assert
            converter.Should().BeOfType<XmlConverter>();
        }
    }
}
