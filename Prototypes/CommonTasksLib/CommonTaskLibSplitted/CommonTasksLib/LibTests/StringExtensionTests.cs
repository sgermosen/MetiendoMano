using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonTasksLib.Structs;

namespace LibTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void FormatWith_ValidFormat()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            TestClass objb = new TestClass
            {
                FirstProperty = "Second instance",
                ReferenceProperty = DateTime.Now.AddDays(-1).AddSeconds(-100),
                SelfReferencedProperty = obja
            };
            obja.SelfReferencedProperty = objb;
            string format = "First : {FirstProperty} \n" +
                              "Second : {ReferenceProperty}\n" +
                              "Inner : {SelfReferencedProperty.SomeMethod()} \n";
            string firstLine = "First : Hi ";
            string formattedString = obja.FormatWith(format);
            string actualLIne = formattedString.Substring(0, formattedString.IndexOf('\n'));
            Console.WriteLine(obja.FormatWith(format));
            Console.WriteLine();
            Assert.AreEqual(firstLine, actualLIne, "Format With Failed");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatWith_NullFormat()
        {
            TestClass obja = new TestClass
            {
                FirstProperty = "Hi",
                ReferenceProperty = DateTime.Now
            };
            TestClass objb = new TestClass
            {
                FirstProperty = "Second instance",
                ReferenceProperty = DateTime.Now.AddDays(-1).AddSeconds(-100),
                SelfReferencedProperty = obja
            };
            obja.SelfReferencedProperty = objb;
            string format = "First : {FirstProperty} \n" +
                              "Second : {ReferenceProperty}\n" +
                              "Inner : {SelfReferencedProperty.SomeMethod()} \n";
            string firstLine = "First : Hi ";
            string formattedString = obja.FormatWith(null);
            string actualLIne = formattedString.Substring(0, formattedString.IndexOf('\n'));
            Console.WriteLine(obja.FormatWith(format));
            Console.WriteLine();
            Assert.AreEqual(firstLine, actualLIne, "Format With Failed");
        }

        [TestMethod]
        public void Right_EmpyString()
        {
            string str = string.Empty;
            var result = str.Right(5);

            Assert.AreEqual(str, string.Empty, "Right Most String are not the same as expected");
        }

        [TestMethod]
        public void Right_NoEmptyString()
        {
            string str = "1234567890";
            string expected = "67890";
            var result = str.Right(5);

            Assert.AreEqual(expected, result, "Right Most String are not the same as expected");
        }

        [TestMethod]
        public void Right_HigherLength_ThanString()
        {
            string str = "1234567890";
            string expected = "1234567890";
            var result = str.Right(11);

            Assert.AreEqual(expected, result, "Right Most String are not the same as expected");
        }

        [TestMethod]
        public void Right_NegativeValue()
        {
            string str = "1234567890";
            string expected = string.Empty;
            var result = str.Right(-1);

            Assert.AreEqual(expected, result, "Right Most String are not the same as expected");
        }

        [TestMethod]
        public void Left_EmpyString()
        {
            string str = string.Empty;
            var result = str.Left(5);

            Assert.AreEqual(str, string.Empty, "Left Most String are not the same as expected");
        }

        [TestMethod]
        public void Left_NoEmptyString()
        {
            string str = "1234567890";
            string expected = "12345";
            var result = str.Left(5);

            Assert.AreEqual(expected, result, "Left Most String are not the same as expected");
        }

        [TestMethod]
        public void Left_HigherLength_ThanString()
        {
            string str = "1234567890";
            string expected = "1234567890";
            var result = str.Left(11);

            Assert.AreEqual(expected, result, "Left Most String are not the same as expected");
        }

        [TestMethod]
        public void Left_NegativeValue()
        {
            string str = "1234567890";
            string expected = string.Empty;
            string result = str.Left(-1);

            Assert.AreEqual(expected, result, "Left Most String are not the same as expected");
        }
    }
}
