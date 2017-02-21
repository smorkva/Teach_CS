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
            for (int i = 0; i < 100000; i++)
            {
                var _result = CalcFibonachi.MethodOne(50);
                Assert.AreEqual(_50th, _result);
            }
        }
        [TestMethod]
        public void TestMethodTwo()
        {
            var _50th = 12586269025;
            for (int i = 0; i < 100000; i++)
            {
                var _result = CalcFibonachi.MethodTwo(50);
                Assert.AreEqual(_50th, _result);
            }
        }
        [TestMethod]
        public void TestMethodThree()
        {
            var _50th = 12586269025;
            for (int i = 0; i < 100000; i++)
            {
                var _result = CalcFibonachi.MethodThree(50);
                Assert.AreEqual(_50th, _result);
            }
        }
        [TestMethod]
        public void TestMethodFour()
        {
            var _50th = 12586269025;
            for (int i = 0; i < 100000; i++)
            {
                var _result = CalcFibonachi.TestMethodFour(50);
                Assert.AreEqual(_50th, _result);
            }
        }
    }
}
