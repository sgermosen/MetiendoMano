using Common;
using FrontEnd.ViewModels;
using FrontEnd.App_Start;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(TestViewModel model)
        {
            var rh = new ResponseHelper();

            if (!ModelState.IsValid)
            {
                var validations = ModelState.GetErrors();
                rh.SetValidations(validations);
            }

            return Json(rh);
        }
    }
}