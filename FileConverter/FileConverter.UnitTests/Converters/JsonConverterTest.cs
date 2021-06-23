using FileConverter.Core.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FileConverter.UnitTests
{
    [TestClass]
    public class JsonConverterTest
    {
        [TestMethod]
        public void GIVEN_SourceWithoutChildrenProperties_WHEN_ConvertToJsonFormat_THEN_JsonFormatShouldBeCorrect()
        {
            //Arrange
            var intermediateModel = new Dictionary<string, object> { 
                { "p1", "v1" },
                { "p2", "v2" },
                { "p3", "v3" }
            };
            var converter = new JsonConverter();

            //Act
            var jsonFormat = converter.ConvertFromIntermediateModel(intermediateModel);


            //Assert
            jsonFormat.Should().Be("{\"p1\":\"v1\",\"p2\":\"v2\",\"p3\":\"v3\"}");
        }


        [TestMethod]
        public void GIVEN_SourceWithChildrenProperties_WHEN_ConvertToJsonFormat_THEN_JsonFormatShouldBeCorrect()
        {
            //Arrange
            var intermediateModel = new Dictionary<string, object> {
                { "p1",  new Dictionary<string, object> { { "p1.1", "v1.1"}, {"p1.2", "v1.2" } } },
                { "p2", "v2" },
                { "p3", "v3" }
            };
            var converter = new JsonConverter();

            //Act
            var jsonFormat = converter.ConvertFromIntermediateModel(intermediateModel);


            //Assert
            jsonFormat.Should().Be("{\"p1\":{\"p1.1\":\"v1.1\",\"p1.2\":\"v1.2\"},\"p2\":\"v2\",\"p3\":\"v3\"}");
        }
    }
}