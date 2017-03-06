using System.Collections.Generic;
using Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExtension
{
    [TestClass]
    public class TestExtension
    {
        [TestMethod]
        public void CollectionIntSum()
        {
            var transport = new List<int>() { 1, 2, 3, 4, 5 };
            var result = transport.SumByPredicate(x => x > 3);

            Assert.AreEqual(9, result);
        }
        [TestMethod]
        public void CollectionDoubleSum()
        {
            var transport = new List<double>() { 1.02, 2.486, 3.146, 4.47, 5.145 };
            var result = transport.SumByPredicate(x => x > 3);

            Assert.AreEqual(12.761, result);
        }
        [TestMethod]
        public void CollectionLongSum()
        {
            var transport = new List<long>() { 1, 2, 3, 4, 5 };
            var result = transport.SumByPredicate(x => x > 3);

            Assert.AreEqual(9, result);
        }
    }
}
