using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mJSON;

namespace mJSONTest
{
    [TestClass]
    public class SerializeTest
    {
        [TestMethod]
        public void NullTest()
        {
            var json = "null";
            var item = default(string);
            var result = JsonConverter.SerializeObject(item);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void NullableTest()
        {
            var testData = new Dictionary<string, object>();
            testData.Add("{\"NInt\":null,\"NFloat\":null,\"NDouble\":null}", default(nullableStruct));
            testData.Add("{\"NInt\":1,\"NFloat\":0.254,\"NDouble\":0.4568}", new nullableStruct() { NInt = 1, NFloat = 0.254F, NDouble = 0.4568D});

            foreach (var pair in testData)
            {
                var result = JsonConverter.SerializeObject(pair.Value);
                Assert.AreEqual(pair.Key, result);
            }
        }
        [TestMethod]
        public void SimpleTest()
        {
            var testData = new Dictionary<string, object>();
            testData.Add("123", 123);
            testData.Add("\"str\"ing\"", "str\"ing");
            testData.Add("13", 13U);
            testData.Add("15.486", 15.486);

            foreach (var pair in testData)
            {
                var result = JsonConverter.SerializeObject(pair.Value);
                Assert.AreEqual(pair.Key, result);
            }
        }
        [TestMethod]
        public void StructTest()
        {
            var json = "{\"Name\":\"string\",\"Data\":1}";
            var item = new testStruct() { Name = "string", Data = 1 };

            var result = JsonConverter.SerializeObject(item);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void ArrayTest()
        {
            var json = "[1,4,76,2,6]";
            var items = new int[] { 1, 4, 76, 2, 6 };

            var result = JsonConverter.SerializeObject(items);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void ListTest()
        {
            var json = "[1,4,76,2,6]";
            var items = new List<int> { 1, 4, 76, 2, 6 };

            var result = JsonConverter.SerializeObject(items);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void DictonaryTest()
        {
            var json = "{\"1\":\"one\",\"3\":\"two\",\"2\":\"tree\"}";
            var items = new Dictionary<int, string>
            {
                { 1, "one"},
                { 3, "two"},
                { 2, "tree"}
            };

            var result = JsonConverter.SerializeObject(items);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void ClassTest()
        {
            var json = "[{\"Name\":\"Name\",\"Data\":2,\"linked\":{\"A\":1,\"B\":\"One\"}},{\"NewField\":\"string\",\"Name\":\"Name\",\"Data\":2,\"linked\":{\"A\":2,\"B\":\"Two\"}}]";
            var items = _arrayOfClass();
            var result = JsonConverter.SerializeObject(items);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedStructTest()
        {
            var json = IntendedStructResult;
            var item = new testStruct() { Name = "string", Data = 1 };

            var result = JsonConverter.SerializeObject(item, Formatting.Indented);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedArrayTest()
        {
            var json = "[\r\n  1,\r\n  4,\r\n  76,\r\n  2,\r\n  6\r\n]";
            var items = new int[] { 1, 4, 76, 2, 6 };

            var result = JsonConverter.SerializeObject(items, Formatting.Indented);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedListTest()
        {
            var json = "[\r\n  1,\r\n  4,\r\n  76,\r\n  2,\r\n  6\r\n]";
            var items = new List<int> { 1, 4, 76, 2, 6 };

            var result = JsonConverter.SerializeObject(items, Formatting.Indented);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedDictonaryTest()
        {
            var json = IntendedArrrayResult;
            var items = new Dictionary<int, string>
            {
                { 1, "one"},
                { 3, "two"},
                { 2, "tree"}
            };

            var result = JsonConverter.SerializeObject(items, Formatting.Indented | Formatting.IncludeTypeName);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedClassTest()
        {
            var json = IntendedClassResult;
            var items = _arrayOfClass();

            var result = JsonConverter.SerializeObject(items, Formatting.Indented);

            Assert.AreEqual(json, result);
        }
        [TestMethod]
        public void IntendedClassWithTypeNameTest()
        {
            var json = IntendedClassWithTypeResult;
            var items = _arrayOfClass();

            var result = JsonConverter.SerializeObject(items, Formatting.Indented | Formatting.IncludeTypeName);

            Assert.AreEqual(json, result);
        }

        struct nullableStruct
        {
            public int? NInt;
            public float? NFloat;
            public double? NDouble;
        }
        struct testStruct
        {
            public string Name;
            public int Data;
        }
        class linkedClass
        {
            public int A { get; set; } = default(int);
            public string B { get; set; } = default(string);
        }
        class testClass
        {
            public string Name { get; set; } = default(string);
            public int Data { get; set; } = default(int);
            public linkedClass linked { get; set; } = default(linkedClass);
        }
        class successor : testClass
        {
            public string NewField { get; set; } = default(string);
        }
        private testClass[] _arrayOfClass()
        {
            return new testClass[]
            {
                new testClass() { Name = "Name", Data = 2, linked = new linkedClass()
                {
                    A = 1, B = "One"
                } },
                new successor() { Name = "Name", Data = 2 , NewField = "string" , linked = new linkedClass()
                {
                    A = 2, B = "Two"
                }},
            };
        }
        private string IntendedClassResult { get; } = @"[
  {
    ""Name"": ""Name"",
    ""Data"": 2,
    ""linked"": {
      ""A"": 1,
      ""B"": ""One""
    }
  },
  {
    ""NewField"": ""string"",
    ""Name"": ""Name"",
    ""Data"": 2,
    ""linked"": {
      ""A"": 2,
      ""B"": ""Two""
    }
  }
]";
        private string IntendedClassWithTypeResult { get; } = @"[
  {
    ""Name"": ""Name"",
    ""Data"": 2,
    ""linked"": {
      ""A"": 1,
      ""B"": ""One""
    }
  },
  {
    ""$type"": ""mJSONTest.SerializeTest+successor, mJSONTest"",
    ""NewField"": ""string"",
    ""Name"": ""Name"",
    ""Data"": 2,
    ""linked"": {
      ""A"": 2,
      ""B"": ""Two""
    }
  }
]";
        private string IntendedArrrayResult { get; } = @"{
  ""1"": ""one"",
  ""3"": ""two"",
  ""2"": ""tree""
}";
        private string IntendedStructResult { get; } = @"{
  ""Name"": ""string"",
  ""Data"": 1
}";
    }
}
