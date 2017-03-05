using Transport.Shema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseTest
{
    [TestClass]
    public class TestDataTransport
    {
        [TestMethod]
        public void TransportEqualObjectTest()
        {
            var transport = new Bus() { Name = "Some Bus", Number = "ab1234U", WheelCount = 4, MaxSpeed = 110, Route = "A-B" };
            var copy = transport.Clone();
            
            Assert.IsTrue(transport.Equals(copy));
        }
        [TestMethod]
        public void TransportNotEqualChangedObjectTest()
        {
            var transport = new Bus() { Name = "Some Bus", Number = "ab1234U", WheelCount = 4, MaxSpeed = 110, Route = "A-B" };
            var copy = transport.Clone();
            (copy as DataTransport).Number = "123";

            Assert.IsFalse(transport.Equals(copy));
        }
    }
}
