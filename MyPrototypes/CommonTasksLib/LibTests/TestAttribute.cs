using System;

namespace LibTests
{
    class TestAttribute : Attribute
    {
        public string TestData { get; set; }

        public TestAttribute(string TestData)
        {
            this.TestData = TestData;
        }
    }
}
