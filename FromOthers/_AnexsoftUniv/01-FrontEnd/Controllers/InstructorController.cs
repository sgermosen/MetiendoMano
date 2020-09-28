using Common;
using FrontEnd.App_Start;
using FrontEnd.ViewModels;
using Model.Domain;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class InstructorController : Controller
    {
        private ICourseService _courseService = DependecyFactory.GetInstance<ICourseService>();
        private ICategoryService _categoryService = DependecyFactory.GetInstance<ICategoryService>();
        private IInstructorService _instructorService = DependecyFactory.GetInstance<IInstructorService>();
        private ILessonService _lessonService = DependecyFactory.GetInstance<ILessonService>();

        // GET: Course
        [Authorize(Roles = RoleNames.Teacher)]
        public ActionResult Index()
        {
            return View(
                _instructorService.GetAll(CurrentUserHelper.Get.UserId)
            );
        }

        [HttpPost]
        public JsonResult GetWidget()
        {
            var model = _instructorService.Widget(CurrentUserHelper.Get.UserId);
            return Json(model);
        }

        public ActionResult CreateCourse()
        {
            var model = new CourseBasicInformationViewModel();
            model.Categories = _categoryService.GetAll();
            model.Course = new Course();

            return View(model);
        }

        [HttpPost]
        public JsonResult SaveCourse(Course model)
        {
            var rh = new ResponseHelper();
            var newRecord = model.Id == 0;

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                rh = _courseService.InsertOrUpdateBasicInformation(model);

                if (rh.Response && newRecord)
                {
                    rh.Href = "instructor";
                }
            }

            return Json(rh);
        }

        public ActionResult Course(int id)
        {
            var model = new CourseBasicInformationViewModel();
            model.Categories = _categoryService.GetAll();
            model.Course = _courseService.Get(id);

            return View(model);
        }

        [HttpPost]
        public JsonResult AddImage(int id, HttpPostedFileBase file)
        {
            var rh = new ResponseHelper();

            if (file == null)
            {
                return Json(rh.SetResponse(false, "Se requiere la imagen"));
            }

            return Json(
                _courseService.AddImage(id, file)
            );
        }

        #region Lessons
        [HttpPost]
        public JsonResult InsertLesson(LessonCreateViewModel model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                rh = _lessonService.Insert(new LessonsPerCourse {
                    Name = model.Name,
                    CourseId = model.CourseId
                });
            }

            return Json(rh);
        }

        [HttpPost]
        public JsonResult GetAllLessons(int courseId)
        {
            return Json(
                _lessonService.GetAll(courseId)
            );
        }

        [HttpPost]
        public JsonResult GetLesson(int id)
        {
            return Json(
                _lessonService.Get(id)
            );
        }
        #endregion

        [HttpPost, ValidateInput(false)]
        public JsonResult UpdateLesson(LessonUpdateViewModel model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                rh = _lessonService.Update(new LessonsPerCourse
                {
                    Id = model.Id,
                    Name = model.Name,
                    Content = model.Content,
                    Video = model.Video
                });
            }

            return Json(rh);
        }

        [HttpPost]
        public JsonResult OrderLesson(List<LessonOrderViewModel> model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                var lessons = model.Select(x => new LessonsPerCourse
                {
                    Id = x.Id,
                    Order = x.Order
                }).ToList();

                rh = _lessonService.Order(lessons);
            }

            return Json(rh);
        }

        [HttpPost]
        public JsonResult DeleteLesson(int id)
        {
            return Json(_lessonService.Delete(id));
        }
    }
}