using SGC_MVC.Filters;
using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using WebMatrix.WebData;

namespace SGC_MVC.Helpers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class MenuAuthorize
    {
        SASContext db = new SASContext();
        /// <summary>
        /// Constructor para sacar los permisos que tiene un usuario y almacenarlos en la variable $Menu
        /// </summary>
        public MenuAuthorize()
        {
            try
            {
                Tag = "<li>";

                if (WebSecurity.IsAuthenticated)
                {
                    db = new SGC_MVC.Models.SASContext();
                    var user = db.Users.FirstOrDefault(usr => usr.activeKey == "NOT FOUND");

                    user = db.Users.FirstOrDefault(usr => usr.ID == WebSecurity.CurrentUserId);

                    foreach (SGC_MVC.Models.Webpages_Roles role in user.Webpages_Roles)
                    {
                        foreach (SGC_MVC.Models.Action action in role.Actions)
                        {
                            if (!Menu.ContainsKey(
                                        string.Format("{0}-{1}", action.Controller.name.Replace("Controller", ""),
                                        action.name)
                                ))
                            {
                                Menu.Add(
                                    string.Format("{0}-{1}", action.Controller.name.Replace("Controller", ""), action.name),
                                    action.name
                                );
                            }
                        }
                    }
                }
            }
            catch
            {
                new AuthorizationContext().Result = new RedirectToRouteResult(
                     new RouteValueDictionary
                 {
                     { "controller", "Account" },
                     { "action", "Login" }
                 });
            }
        }

        //Variables privadas
        Dictionary<string, string> Menu = new Dictionary<string, string>();

        /// <summary>
        /// Retorna o asigna el tag en encapsula los objetos HTML
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Retorna el tag que cierra la encapsulación para los objetos HTML
        /// </summary>
        public string TagClose
        {
            get { return Tag.Replace("<", "</"); }
        }

        public string getMenuLabel(HttpRequestBase request)
        {
            string[] properties = request.AppRelativeCurrentExecutionFilePath.Split('/');
            string controller = properties[1];
            string action = "";
            if (properties.Count() < 3 || string.IsNullOrEmpty(properties[2]))
                action = "Index";
            else
                action = properties[2];

            SubMenu subMenu = db.SubMenus.Where(d => d.View.Action.Controller.name == controller + "Controller" && d.View.Action.name == action).FirstOrDefault();

            string name = "";
            if (subMenu != null)
                name = subMenu.name;
            return name;
        }

        /// <summary>
        /// Retorna true si el usuario loged acutal tiene permiso al controlador-acción
        /// </summary>
        /// <param name="actionName">Nombre de la acción</param>
        /// <param name="ControllerName">Nombre del controlador</param>
        /// <returns></returns>
        public bool HasPermission(string actionName, string ControllerName)
        {
            if (Menu.Where(d => d.Key == (ControllerName + "-" + actionName) && d.Value == actionName).Count() > 0)
                return true;
            return false;
        }
    }

    public static class MenuAuthorizeMethods
    {

        /// <summary>
        /// Retorna el texto para un link utlizando el _tag por defecto de la clase.
        /// </summary>
        /// <param name="linkText">Texto para el link</param>
        /// <param name="actionName">Accción para el link</param>
        /// <param name="ControllerName">Controlador para el link</param>
        /// <returns></returns>
        public static MvcHtmlString AuthorizeActionLink
            (
            this System.Web.Mvc.HtmlHelper htmlHelper,
            MenuAuthorize menuAuthorize,
            string linkText, string actionName, string ControllerName
            )
        {
            if (menuAuthorize.HasPermission(actionName, ControllerName))
            {

                return htmlHelper.ActionLink(linkText, actionName);

                //return MvcHtmlString.Create(
                //    string.Format("{0}<a href=\"/{5}/{2}/{3}\">{4}</a>{1}",
                //        menuAuthorize.Tag,
                //        menuAuthorize.TagClose,
                //        ControllerName,
                //        actionName,
                //        linkText,
                //        System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
                //        ));
            }
            return MvcHtmlString.Create(
                string.Format("{0}<a class=\"disable\" disabled=\"disabled\">{4}</a>{1}",
                    menuAuthorize.Tag,
                    menuAuthorize.TagClose,
                    ControllerName, actionName,
                    linkText));
        }
    }

}

