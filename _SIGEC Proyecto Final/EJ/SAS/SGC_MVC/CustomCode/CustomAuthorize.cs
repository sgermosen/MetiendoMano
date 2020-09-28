using SGC_MVC.Helpers;
using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebMatrix.WebData;

namespace SGC_MVC.CustomCode
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        SASContext db;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            db = new SASContext();
            if (WebSecurity.IsAuthenticated)
            {
                var a = db.Actions
                    .FirstOrDefault(ac => ac.name == filterContext.ActionDescriptor.ActionName);

                if (new MenuAuthorize().HasPermission(filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName))
                {
                    return;
                }
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary
                 {
                     { "controller", "Unauthorize" },
                     { "action", "ErrorUnauthorized" }
                 });
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                 {
                     { "controller", "Account" },
                     { "action", "Login" }
                 });
            }
        }
    }
}