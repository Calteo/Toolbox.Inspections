using Toolbox.Inspectors;

namespace Toolbox.Inspections.Test
{
    [TestClass]
    public class TypeInspectorTest
    {
        [TestMethod]
        public void EnvironmentTest()
        {
            const string key = "StaticKey";

            var cut = new TypeInspector(key, typeof(Environment));

            var inspections = cut.Inspect()?.ToArray();

            Assert.IsTrue(cut.IsAlive);
            Assert.AreEqual(key, cut.Key);

            Assert.IsNotNull(inspections);
            Assert.AreEqual(1, inspections.Length);
            Assert.AreEqual(key, inspections[0].Key);
        }
    }
}
