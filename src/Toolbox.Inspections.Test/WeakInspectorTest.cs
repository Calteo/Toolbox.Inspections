using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toolbox.Inspectors;

namespace Toolbox.Inspections.Test
{
    [TestClass]
    public class WeakInspectorTest
    {
        [TestMethod]
        public void IsAliveWithReference()
        {
            const string key = "SomeFunnyKey";

            var obj = new object();

            var cut = new WeakInspector(key, obj);

            Assert.IsTrue(cut.IsAlive);
        }

        class SimpleData
        {
            public string Name { get; set; } = "";

            public int Number { get; set; }

            public string? Optional { get; set; }
        }

        [TestMethod]
        public void InspectSimpleData()
        {
            var data = new SimpleData
            {
                Name = "RandomName",
                Number = Random.Shared.Next(1000)
            };

            const string key = "SomeFunnyKey";

            var cut = new WeakInspector(key, data);

            var inspections = cut.Inspect().ToArray();

            Assert.AreEqual(1, inspections.Length);

            var inspection = inspections[0];

            Assert.AreEqual(data.Name, inspection.Values[nameof(data.Name)]);
            Assert.AreEqual(data.Number, inspection.Values[nameof(data.Number)]);
            Assert.AreEqual(null, inspection.Values[nameof(data.Optional)]);
        }
    }
}
