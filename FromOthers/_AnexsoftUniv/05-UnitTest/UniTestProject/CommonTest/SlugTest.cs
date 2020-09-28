using Common.MyExtensions;
using Common.ProjectHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniTestProject.CommonTest
{
    [TestClass]
    public class SlugTest
    {
        [TestMethod]
        public void Course()
        {
            var result = "course/1/nuevo-curso-de-javascript-y-css-3";
            var original = Slug.Course(1, "Nuevo curso de Javascript y CSS 3".Sluglify());

            Assert.IsTrue(original == result);
        }

        [TestMethod]
        public void Category()
        {
            var result = "category/1/marketing";
            var original = Slug.Category(1, "marketing".Sluglify());

            Assert.IsTrue(original == result);
        }
    }
}
