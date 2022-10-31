using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toolbox.Inspectors;

namespace Toolbox.Inspections.Test
{
    [TestClass]
    public class AttributesTest
    {
        class DataClass
        {
            public DataClass()
            {
                Number = new Random().Next(10000);
            }

            public string Name { get; set; } = "SomeName";
            public int Number { get; set; }

            [NoInspection]
            public string Protected { get; set; } = "SuperSecretContent";
        }

        [TestMethod]
        public void HideProperty()
        {
            var data = new DataClass();

            var cut = new WeakInspector("SomeKey", data);

            var inspection = cut.Inspect().First();

            Assert.IsFalse(inspection.Values.ContainsKey(nameof(data.Protected)));
        }

        [TestMethod]
        public void HidePropertyOnForeignClass()
        {
            var cut = new TypeInspector("SomeKey", typeof(Environment));

            NoInspectionAttribute.Set(typeof(Environment), nameof(Environment.StackTrace));

            var inspection = cut.Inspect().First();

            var property = typeof(Environment).GetProperty(nameof(Environment.StackTrace)) ?? throw new NullReferenceException();

            Assert.IsTrue(NoInspectionAttribute.IsSet(property));
            Assert.IsFalse(inspection.Values.ContainsKey(nameof(Environment.StackTrace)));
        }

        [TestMethod]
        public void SetCheckClearNoInspectionOnForeignClass()
        {
            const string name = "SomeProperty";

            NoInspectionAttribute.Set<DataClass>(name);

            Assert.IsTrue(NoInspectionAttribute.IsSet<DataClass>(name));

            NoInspectionAttribute.Clear<DataClass>(name);

            Assert.IsFalse(NoInspectionAttribute.IsSet<DataClass>(name));
        }
    }
}

