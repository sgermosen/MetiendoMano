using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
