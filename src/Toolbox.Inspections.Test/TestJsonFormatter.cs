using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Toolbox.Inspectors;
using Toolbox.Serialization;

namespace Toolbox.Inspections.Test
{
    [TestClass]
    public class TestJsonFormatter
    {
        #region SimpleData
        class SimpleData
        {
            public string Name { get; set; } = "SomeSimpleName";
            public int Number { get; set; }
            public bool Ok { get; set; }                        

            public string? Optional { get; set; }
        }
        #endregion

        #region ComplexData
        class ComplexData
        {
            public Rectangle Bounds { get; set; } = new Rectangle(10,20, 200, 100);
            public SimpleData SimpleData { get; } = new SimpleData();
        }
        #endregion

        [TestMethod]
        public void SerializeSimpleData()
        {
            const string key = "SimpleData/SubData";
            const string name = "MySimpleData";
            const int number = 4501;

            var data = new SimpleData
            {
                Name = name,
                Number = number,
                Ok = true
            };

            var inspector = new WeakInspector(key, data);

            var cut = new JsonInspectionFormatter();

            var stream = new MemoryStream();

            var inspections = inspector.Inspect();

            cut.Serialize(stream, inspections);

            stream.Position = 0;

            var result = JsonNode.Parse(stream);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonArray));

            var text = result.ToJsonString();

            var expected = $"[{{\"key\":\"{key}\",\"values\":{{\"Name\":\"{name}\",\"Number\":{number},\"Ok\":true,\"Optional\":null}}}}]";

            Assert.AreEqual(expected, text);
        }

        [TestMethod]
        public void SerializeComplexData()
        {
            var data = new ComplexData();

            var inspector = new WeakInspector("ComplexData", data);

            var cut = new JsonInspectionFormatter();

            var stream = new MemoryStream();

            var inspections = inspector.Inspect();

            cut.Serialize(stream, inspections);

            stream.Position = 0;

            var result = JsonNode.Parse(stream);

            Assert.IsNotNull(result);

            var text = result.ToJsonString();

            var expected = "[{\"key\":\"ComplexData\",\"values\":{\"Bounds\":\"{X=10,Y=20,Width=200,Height=100}\"},\"inspections\":[{\"key\":\"SimpleData\",\"values\":{\"Name\":\"SomeSimpleName\",\"Number\":0,\"Ok\":false,\"Optional\":null}}]}]";

            Assert.AreEqual(expected, text);
        }
    }
}
