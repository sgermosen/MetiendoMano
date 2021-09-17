using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTaskTests
{
    public class TestClass
    {
        public String FirstProperty { get; set; }
        public DateTime ReferenceProperty{ get; set; }
        
        public TestClass SelfReferencedProperty { get; set; }

        public String SomeMethod()
        {
            return this.ReferenceProperty.ToShortTimeString();
        }
    }
}
