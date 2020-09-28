using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VueSpaApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICurrentUserFactory _currentUser;

        public HomeController(
            ICurrentUserFactory currentUser
        )
        {
            _currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
