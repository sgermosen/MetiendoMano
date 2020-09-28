using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Controllers
{
    public class UnauthorizeController : System.Web.Mvc.Controller
    {
        //
        // GET: /Unauthorize/

        public ActionResult ErrorUnauthorized()
        {
            return View();
        }

    }
}
