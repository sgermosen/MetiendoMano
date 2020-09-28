using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Web.Mvc;

namespace SGC_MVC.Old
{
    public class CustomAuthorize : AuthorizeAttribute
    {

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //    filterContext.Result = new HttpStatusCodeResult(403);
            //    if (filterContext.HttpContext.Request.IsAuthenticated)
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new


            List<String> access = new List<string>();

            access.Add("Index");
            access.Add("Home");

            //if (access.Exists(d => d == filterContext.ActionDescriptor.ControllerDescriptor.ControllerName))
            //{
            //    filterContext.Result = new HttpStatusCodeResult(403);
            //}

            if (access.Exists(d => d == filterContext.ActionDescriptor.ActionName))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }


        }

    }
}