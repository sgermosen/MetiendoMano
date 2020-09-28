using Common;
using Common.ProjectHelpers;
using Model.Domain;
using Service;
using System.IO;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = RoleNames.Student)]
    public class LearnController : Controller
    {
        private IStudentService _studentService = DependecyFactory.GetInstance<IStudentService>();

        [Route("learn/{id}/{lessonId?}")]
        public ActionResult Index(int id, int lessonId = 0)
        {
            return View(
                _studentService.Get(
                    id,
                    lessonId,
                    CurrentUserHelper.Get.UserId
                )
            );
        }

        [HttpPost]
        [Route("learn/MarkAsLearned")]
        public JsonResult MarkAsLearned(int lessonId, int courseId)
        {
            var rh = _studentService.MarkAsLearned(
                courseId,
                lessonId,
                CurrentUserHelper.Get.UserId
            );

            if (rh.Response)
            {
                rh.Href = "self";
            }

            return Json(
                rh
            );
        }
    }
}