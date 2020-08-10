using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonTasksLib.Data;

namespace LibTests
{
    [TestClass]
    public class ObjectExtensionTests
    {
        [TestMethod]
        public void Transfer_NullDestination()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            TestClass objb = null;

            obja.Transfer(ref objb);

            Assert.AreEqual(obja.FirstProperty, objb.FirstProperty, "Transfer operation failed.");
        }

        [TestMethod]
        public void Transfer_USingSkip()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            TestClass objb = null;

            obja.Transfer(ref objb, "FirstProperty, ReferenceProperty");

            Assert.AreEqual(null, objb.FirstProperty, "Transfer operation failed.");
        }

        [TestMethod]
        public void ToString_Default()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };

            string formattedString = obja.ToString<TestClass>();
            string expected = "Hi";
            string result = formattedString.Split('\n')[0].Split(':')[1].TrimEnd().TrimStart();

            Assert.AreEqual(expected, result, "ToString operation failed.");
        }

        [TestMethod]
        public void ToString_CustomSeparator()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };

            string formattedString = obja.ToString<TestClass>("|");
            int expected = 3;
            int result = formattedString.Split('|').Length;

            Assert.AreEqual(expected, result, "ToString operation failed.");
        }

        [TestMethod]
        public void GetCustomAttribute_Default()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            string expected = "Expected";

            var attribute = obja.GetCustomAttribute<TestAttribute>();

            Assert.AreEqual(expected, attribute.TestData, "Attribute not found");
        }

        [TestMethod]
        public void GetProperty_Default()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            string expected = "FirstProperty";

            var property = obja.GetProperty(t => t.FirstProperty);

            Assert.AreEqual(expected, property.Name, "Property not found");
        }

        [TestMethod]
        public void GetCustomAttribute_FromProperty()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            string expected = "First Property";

            var attribute = obja.GetCustomAttribute<TestClass, string, TestAttribute>(t => t.FirstProperty);

            Assert.AreEqual(expected, attribute.TestData, "Attribute not found");
        }

        [TestMethod]
        public void GetCustomAttribute_CustomProvider()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            string expected = "Expected";

            var type = obja.GetType();
            var actual = type.GetCustomAttribute<TestAttribute, Type>().TestData;

            Assert.AreEqual(expected, actual, "Attribute not found");
        }


        [TestMethod]
        public void ConvertTo_Unboxing()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            var expected = typeof(TestAttribute);
            var attr = obja.GetType().GetCustomAttribute<Attribute, Type>();
            var actual = attr.ConvertTo<TestAttribute>();

            Assert.AreEqual(expected, actual.GetType(), "Change type not sucessfull");
        }
    }
}
