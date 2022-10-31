using Toolbox.Inspectors;

namespace Toolbox.Inspections.Test
{
    [TestClass]
    public class InspectorTest
    {
        [TestMethod]
        public void TestKeyFromContructor()
        {
            const string key = "SomeFunnyKey";

            var cut = new Inspector(key);

            Assert.AreEqual(key, cut.Key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowOnNullKeyFromContructor()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string key = null; ;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
            var cut = new Inspector(key);
#pragma warning restore CS8604 // Possible null reference argument.

            Assert.AreEqual(key, cut.Key);
        }
    }
}