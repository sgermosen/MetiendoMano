using Common;
using Service;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService = DependecyFactory.GetInstance<ICourseService>();
        private ICategoryService _categoryService = DependecyFactory.GetInstance<ICategoryService>();

        [Route("course/{id}/{slug}")]
        public ActionResult Index(int id, string slug)
        {
            return View(
                _courseService.GetForLandingPage(id)
            );
        }

        [Route("category/{id}/{slug}")]
        public ActionResult Category(int id, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return Redirect("~");
            }

            var model = _categoryService.Get(id);

            ViewBag.Title = model.Name;
            return View(model);
        }

        [HttpPost]
        public JsonResult All(int categoryId)
        {
            return Json(
                _courseService.GetAll(categoryId)
            );
        }

        [HttpPost, Authorize]
        public JsonResult Purchase(int courseId)
        {
            var rh = _courseService.Purchase(
                courseId,
                CurrentUserHelper.Get.UserId
            );

            if (rh.Response)
            {
                rh.Href = "studying";
            }

            return Json(
                rh
            );
        }
    }
}