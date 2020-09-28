using Common.MyExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniTestProject.CommonTest
{
    [TestClass]
    public class SluglifyTest
    {

        [TestMethod]
        public void SluglifyTest1()
        {
            var result = "nuevo-curso-de-javascript-y-css-3";
            var original = "Nuevo curso de Javascript y CSS 3".Sluglify();

            Assert.IsTrue(original == result);
        }

        [TestMethod]
        public void SluglifyTest2()
        {
            var result = "curso-de-aplicaciones-y-metodologias-con-c";
            var original = "curso de aplicaciones y metodologías con C#".Sluglify();

            Assert.IsTrue(original == result);
        }

        [TestMethod]
        public void SluglifyTest3()
        {
            var result = "curso-javascript-y-css-3-avanzado";
            var original = "Curso: Javascript y CSS 3 - Avanzado".Sluglify();

            Assert.IsTrue(original == result);
        }
    }
}
