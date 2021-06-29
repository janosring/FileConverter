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

        [TestMethod]
        public void GIVEN_SourceWithoutChildrenProperties_WHEN_ConvertToIntermediateModel_THEN_ModelShouldBeCorrect()
        {
            //Arrange
            var source = "{\"p1\":\"v1\",\"p2\":\"v2\",\"p3\":\"v3\"}";
            var converter = new JsonConverter();

            //Act
            var intermediateModel = converter.ConvertToIntermediateModel(source);


            //Assert
            intermediateModel.Should().NotBeNull();

            intermediateModel
                .Should().ContainKey("p1")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v1");

            intermediateModel
                .Should().ContainKey("p2")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v2");

            intermediateModel
                .Should().ContainKey("p3")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v3");
        }

        [TestMethod]
        public void GIVEN_SourceWithChildrenProperties_WHEN_ConvertToIntermediateModel_THEN_ModelShouldBeCorrect()
        {
            //Arrange
            var converter = new JsonConverter();
            var source = "{\"p1\":{\"p1p1\":\"v1v1\",\"p1p2\":\"v1v2\"},\"p2\":\"v2\",\"p3\":\"v3\"}";

            //Act
            var intermediateModel = converter.ConvertToIntermediateModel(source);

            //Assert
            intermediateModel.Should().NotBeNull();

            intermediateModel
                .Should().ContainKey("p1")
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p1p1")
                .WhichValue.Should().BeEquivalentTo("v1v1");

            intermediateModel
                .Should().ContainKey("p1")
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p1p2")
                .WhichValue.Should().BeEquivalentTo("v1v2");

            intermediateModel
                .Should().ContainKey("p2")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v2");

            intermediateModel
                .Should().ContainKey("p3")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v3");

        }
    }
}