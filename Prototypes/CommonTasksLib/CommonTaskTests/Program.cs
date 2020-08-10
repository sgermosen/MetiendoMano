using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTaskTests
{
    class Program
    {
        static void Main(string[] args)
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
            Console.WriteLine(objb.FormatWith(format));
            Console.WriteLine("\n\n");
            Console.WriteLine(obja.FormatWith(format));
            Console.ReadKey();
        }
    }
}
