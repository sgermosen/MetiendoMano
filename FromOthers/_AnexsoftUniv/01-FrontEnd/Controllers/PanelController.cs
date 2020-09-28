using Common;
using FrontEnd.App_Start;
using Model.Domain;
using Service;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class PanelController : Controller
    {
        private IUserService _userService = DependecyFactory.GetInstance<IUserService>();
        private ICategoryService _categoryService = DependecyFactory.GetInstance<ICategoryService>();
        private ICourseService _courseService = DependecyFactory.GetInstance<ICourseService>();
        private IWidgetService _widgetService = DependecyFactory.GetInstance<IWidgetService>();

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        #region Category
        public ActionResult Categories()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCategory(int id)
        {
            return Json(
                _categoryService.Get(id)
            );
        }

        [HttpPost]
        public JsonResult GetCategories(AnexGRID grid)
        {
            return Json(
                _categoryService.GetAll(grid)
            );
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            return Json(
                _categoryService.Delete(id)
            );
        }

        [HttpPost]
        public JsonResult CategorySave(Category model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                rh = _categoryService.InsertOrUpdate(model);
                if (rh.Response)
                {
                    rh.Href = "self";
                }
            }

            return Json(rh);
        }
        #endregion

        #region Course
        public ActionResult Courses()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCourses(AnexGRID grid)
        {
            return Json(
                _courseService.GetAll(grid)
            );
        }

        [HttpPost]
        public JsonResult ChangeStatusByCourse(int id, Enums.Status status)
        {
            return Json(
                _courseService.ChangeStatus(id, status)
            );
        }
        #endregion

        #region Users
        public ActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUsers(AnexGRID grid)
        {
            return Json(
                _userService.GetAll(grid)
            );
        }
        #endregion

        #region Widget
        [HttpPost]
        public JsonResult WidgetGetIncomes(bool month)
        {
            return Json(
                _widgetService.Incomes(month)
            );
        }

        [HttpPost]
        public JsonResult WidgetGetVariance()
        {
            return Json(
                _widgetService.Variance()
            );
        }

        [HttpPost]
        public JsonResult WidgetGetAverage()
        {
            return Json(
                _widgetService.Average()
            );
        }
        #endregion
    }
}