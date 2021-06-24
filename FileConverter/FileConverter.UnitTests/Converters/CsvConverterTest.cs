using FileConverter.Core.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FileConverter.UnitTests
{
    [TestClass]
    public class CsvConverterTest
    {
        [TestMethod]
        public void GIVEN_SourceWithoutChildrenProperties_WHEN_ConvertToIntermediateModel_THEN_IntermediateModelShouldBeCorrect()
        {
            //Arrange
            var converter = new CsvConverter();
            var source = $"p1,p2,p3{Environment.NewLine}v1,v2,v3";

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
        public void GIVEN_SourceWithChildrenProperties_WHEN_ConvertToIntermediateModel_THEN_IntermediateModelShouldBeCorrect()
        {
            //Arrange
            var converter = new CsvConverter();
            var source = $"p1,p2_p2p1,p2_p2p2,p3,p3_p3p1,p4{Environment.NewLine}v1,v2.1,v2.2,v3,v3.1,v4";

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
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p2p1")
                .WhichValue.Should().BeEquivalentTo("v2.1");

            intermediateModel
                .Should().ContainKey("p2")
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p2p2")
                .WhichValue.Should().BeEquivalentTo("v2.2");

            intermediateModel
               .Should().ContainKey("p3")
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p3")
                .WhichValue.Should().BeEquivalentTo("v3");

            intermediateModel
               .Should().ContainKey("p3")
                .WhichValue.Should().BeOfType<Dictionary<string, object>>()
                .Which.Should().ContainKey("p3p1")
                .WhichValue.Should().BeEquivalentTo("v3.1");

            intermediateModel
                .Should().ContainKey("p4")
                .WhichValue.Should().BeOfType<string>()
                .Which.Should().BeEquivalentTo("v4");
        }
    }
}
