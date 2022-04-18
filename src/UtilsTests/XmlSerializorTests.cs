using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Tests
{
    [TestClass()]
    public class XmlSerializorTests
    {
        [TestMethod()]
        public void ToXmlTest()
        {
            var obj = new TestModel
            {
                Key = "Test",
                Children = new List<TestModel>()
                {
                    new TestModel{Key="Child1"},
                    new TestModel{Key="Child2"}
                }
            };
            var xml = obj.ToXml(removeDefaultNamespaces: true);
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml));
        }
    }

    [XmlRoot("Root")]
    public class TestModel
    {
        public string Key { get; set; }

        [XmlElement("TestModel")]
        public List<TestModel> Children { get; set; }
    }
}