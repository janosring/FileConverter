using FileConverter.Core.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileConverter.UnitTests
{
    [TestClass]
    public class CsvConverterTest
    {
        [TestMethod]
        public void GIVEN_SourceWithoutChildProperties_WHEN_ConvertToIntermediateModel_THEN_IntermediateModelShouldBeCorrect()
        {
            //Arrange
            var converter = new CsvConverter();
            var source = $"p1,p2,p3{Environment.NewLine}v1,v2,v3";

            //Act
            var intermediateModel = converter.ConvertToIntermediateModel(source);

            //Assert
            intermediateModel.Should().NotBeNull();

            intermediateModel.Should().ContainKey("p1");
            intermediateModel["p1"].Should().Be("v1");

            intermediateModel.Should().ContainKey("p2");
            intermediateModel["p2"].Should().Be("v2");

            intermediateModel.Should().ContainKey("p3");
            intermediateModel["p3"].Should().Be("v3");
        }
    }
}
