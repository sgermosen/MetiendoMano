using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.MyExtensions;

namespace UniTestProject.CommonTest
{
    [TestClass]
    public class MyExtensionTest
    {
        [TestMethod]
        public void LeadingZeroTest()
        {
            var original = "002";
            var result = 2.LeadingZeros(3);

            Assert.IsTrue(original == result);
        }
    }
}
