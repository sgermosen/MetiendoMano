using Common;
using FrontEnd.App_Start;
using FrontEnd.ViewModels;
using Model.Domain;
using Service;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class StudyingController : Controller
    {
        private IStudentService _studentService = DependecyFactory.GetInstance<IStudentService>();

        public ActionResult Index()
        {
            return View(
                _studentService.GetAll(CurrentUserHelper.Get.UserId)
            );
        }

        public JsonResult Review(ReviewViewModel model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }
            else
            {
                rh = _studentService.Review(new ReviewsPerCourse {
                    Comment = model.Comment,
                    Vote = model.Vote,
                    UserId = CurrentUserHelper.Get.UserId,
                    CourseId = model.CourseId
                });

                if (rh.Response)
                {
                    rh.Href = "self";
                }
            }

            return Json(rh);
        }
    }
}