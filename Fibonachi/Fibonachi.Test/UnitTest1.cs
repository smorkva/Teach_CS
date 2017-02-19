using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fibonachi;

namespace Fibonachi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodOne()
        {
            var _50th = 12586269025;
            var _result = CalcFibonachi.MethodOne(50);// Very, very slow
            
            Assert.AreEqual(_50th, _result);
        }
        [TestMethod]
        public void TestMethodTwo()
        {
            var _50th = 12586269025;
            var _result = CalcFibonachi.MethodTwo(50);
            
            Assert.AreEqual(_50th, _result);
        }
        [TestMethod]
        public void TestMethodThree()
        {
            var _50th = 12586269025;
            var _result = CalcFibonachi.MethodThree(50);

            Assert.AreEqual(_50th, _result);
        }
    }
}
