using Common;
using Service;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService = DependecyFactory.GetInstance<IUserService>();

        [HttpPost]
        public JsonResult GetCreditsByCurrentUser()
        {
            return Json(
                _userService.GetCredits(
                    CurrentUserHelper.Get.UserId
                )
            );
        }
    }
}