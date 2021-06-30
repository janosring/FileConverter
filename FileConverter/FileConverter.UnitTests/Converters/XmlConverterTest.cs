using FileConverter.Core.Converters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FileConverter.UnitTests
{
    [TestClass]
    public class XmlConverterTest
    {       

        [TestMethod]
        public void GIVEN_SourceWithoutChildrenProperties_WHEN_ConvertToXmlFormat_THEN_XmlShouldBeCorrect()
        {
            //Arrange
            var converter = new CsvConverter();
            var source = new Dictionary<string, object>{
               {"p1", "v1" },
               {"p2", "v2" },
               {"p3", "v3" },
           };

            //Act
            var intermediateModel = converter.ConvertFromIntermediateModel(source);

            //Assert
            intermediateModel.Should()
                .Be($"<?xml version=\"1.0\" encoding=\"UTF - 8\"?>{Environment.NewLine}<root>{Environment.NewLine}<p1>v1</p1>{Environment.NewLine}<p2>v2</p2>{Environment.NewLine}<p3>v3</p3>{Environment.NewLine}</root>");
        }

        [TestMethod]
        public void GIVEN_SourceWithChildrenProperties_WHEN_ConvertToXmlFormat_THEN_XmlShouldBeCorrect()
        {
            //Arrange
            var converter = new CsvConverter();
            var source = new Dictionary<string, object>()
            {
                {"p1", "v1"},
                {"p2", new Dictionary<string,object>(){ {"p2p1", "v2.1" }, {"p2p2", "v2.2" } } },
                {"p3", new Dictionary<string,object>(){ {"p3p1", "v3.1" }} },
                {"p4", "v4" }
            };

            //Act
            var intermediateModel = converter.ConvertFromIntermediateModel(source);

            //Assert
            intermediateModel.Should().Be($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{Environment.NewLine}<root>{Environment.NewLine}<p1>v1</p1>{Environment.NewLine}<p2>{Environment.NewLine}<p2p1>v2.1</p2p1>{Environment.NewLine}<p2p2>v2.2</p2p2>{Environment.NewLine}</p2>{Environment.NewLine}<p3>{Environment.NewLine}<p3p1>v3.1</p3p1>{Environment.NewLine}</p3>{Environment.NewLine}<p4>v4</p4>{Environment.NewLine}</root>");
        }
    }
}
