using System;

namespace LibTests
{
    [Test("Expected")]
    public class TestClass
    {
        [Test("First Property")]
        public String FirstProperty { get; set; }
        public DateTime ReferenceProperty { get; set; }

        public TestClass SelfReferencedProperty { get; set; }

        public String SomeMethod()
        {
            return this.ReferenceProperty.ToShortTimeString();
        }
    }
}
