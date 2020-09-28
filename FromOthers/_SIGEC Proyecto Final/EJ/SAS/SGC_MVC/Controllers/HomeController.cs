using SGC_MVC.CustomCode;
using SGC_MVC.Filters;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SGC_MVC.Controllers
{

    [IsMenu]
    public class HomeController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            base.OnActionExecuted(filterContext);
        }

        [IsView]
        public ActionResult Index()
        {
            ViewBag.Message = "Dash Board";
            ViewBag.Advertisements = db.Advertisements.ToList();
            ViewBag.UserTasks = db.UserTasks.ToList();
            ViewBag.Documents = db.Documents.Where(d => d.Status.name == "Creacion").OrderByDescending(d => d.ID).ToList();
            ViewBag.OthersDocuments = db.Documents.Where(d => d.statusID == null).OrderByDescending(d => d.ID).ToList();

            return View();
        }


        public string data()
        {
            int? cid = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            return Helper.getDataJson(Request, cid, db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <param name="DisplayName"></param>
        /// <returns></returns>
        public object CheckUniqGeneral(string validateUniq)
        {
            string wherePlus = "";
            if (!Request.UrlReferrer.Segments.Contains("Create") &&
                !Request.UrlReferrer.Segments.Contains("Register"))
            {
                string id = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Count() - 1];
                wherePlus = string.Format("AND ID!={0}", id);
            }

            string value = Request.QueryString[0];
            string fieldName = Request.QueryString.AllKeys[0];
            int companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
            string[] parameter = validateUniq.Split('&');
            string entityName = parameter[0], DisplayName = parameter[1];
            string select = string.Format("select count(*) from dbo.[{0}] where [{1}]=", entityName, fieldName);
            string query = select + "{0} and (companyID = {1} OR companyID is null) " + wherePlus;
            object[] parameters = { value, companyID };
            var result = db.Database.SqlQuery<int>(query, parameters).ToList();
            if (result[0] > 0)
            {
                return Json(
                    string.Format("El {0} {1} esta en uso, elija un nombre diferente", DisplayName, value),
                    JsonRequestBehavior.AllowGet
                );
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public object CheckUniq(string validateUniq)
        {
            if (string.IsNullOrEmpty(validateUniq))
            {
                validateUniq = Request.QueryString[1];
            }
            string[] parameter = validateUniq.Split('&');
            string entityName = parameter[0], DisplayName = parameter[1];
            string wherePlus = "";
            if (!Request.UrlReferrer.Segments.Contains("Create"))
            {
                string id = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Count() - 1];
                if (entityName == "Webpages_Roles")
                    wherePlus = string.Format("AND RoleID!={0}", id);
                else
                    wherePlus = string.Format("AND ID!={0}", id);
            }

            string value = Request.QueryString[0];
            string fieldName = Request.QueryString.AllKeys[0];
            string select = string.Format("select count(*) from dbo.[{0}] where [{1}]=", entityName, fieldName);
            string query = select + "{0} " + wherePlus;
            object[] parameters = { value };
            var result = db.Database.SqlQuery<int>(query, parameters).ToList();
            if (result[0] > 0)
            {
                return Json(
                    string.Format("El {0} {1} esta en uso, elija un nombre diferente", DisplayName, value),
                    JsonRequestBehavior.AllowGet
                );
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public object CheckKeys(string validateComposite)
        {
            string action = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Count() - 2];
            string id = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Count() - 1];
            string wherePlus = "";
            if (action.ToLower().Contains("edit"))
            {
                wherePlus = string.Format("AND ID!={0}", id);
            }
            string Table = validateComposite.Split('(')[0];
            string field1 = validateComposite.Split('(')[1].Split(',')[0];
            string field2 = validateComposite.Split('(')[1].Split(',')[1].Split(')')[0];
            string display1 = validateComposite.Split('&')[1];
            string display2 = validateComposite.Split('&')[2];

            string firstFieldSwap = Request.QueryString.AllKeys[0];
            string value1 = Request.QueryString[0];
            string value2 = Request.QueryString[2];

            if (firstFieldSwap != field1)
            {
                string valueTemp = "";
                valueTemp = value1;
                value1 = value2;
                value2 = valueTemp;
            }

            if (string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(value2))
                return Json(true, JsonRequestBehavior.AllowGet);

            string query = string.Format("select count(*) from dbo.[{0}] where [{1}]={2} AND [{3}]={4} {5}", Table, field1, "{0}", field2, "{1}", wherePlus);
            object[] parameters = { value1, value2 };
            var result = db.Database.SqlQuery<int>(query, parameters).ToList();
            if (result[0] > 0)
                return Json(string.Format("Ya existe esta combinación de {0} y {1}", display1, display2), JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        public object CheckKeysGeneral(string validateComposite)
        {
            string wherePlus = "";
            if (!Request.UrlReferrer.Segments.Contains("Create"))
            {
                string id = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Count() - 1];
                wherePlus = string.Format("AND ID!={0}", id);
            }

            if (string.IsNullOrEmpty(validateComposite))
                validateComposite = Request.QueryString[1];
            string Table = validateComposite.Split('(')[0];
            string field1 = validateComposite.Split('(')[1].Split(',')[0];
            string field2 = validateComposite.Split('(')[1].Split(',')[1].Split(')')[0];
            string display1 = validateComposite.Split('&')[1];
            string display2 = validateComposite.Split('&')[2];

            string firstFieldSwap = Request.QueryString.AllKeys[0];
            string value1 = Request.QueryString[0];
            string value2 = Request.QueryString[2];

            if (string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(value2))
                return Json(true, JsonRequestBehavior.AllowGet);

            int companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
            string query = string.Format("select count(*) from dbo.[{0}] where [{1}]={2} AND [{3}]={4}  AND companyID = {5} {6}", Table, field1, "{0}", field2, "{1}", companyID, wherePlus);
            object[] parameters = { value1, value2 };
            var result = db.Database.SqlQuery<int>(query, parameters).ToList();
            if (result[0] > 0)
                return Json(string.Format("Ya existe esta combinación de {0} y {1}", display1, display2), JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize]
        [IsView]
        public ActionResult About()
        {
            ViewBag.Message = "SAS, Acerca de.";

            return View();
        }

        [IsView]
        public ActionResult Contact()
        {
            ViewBag.Message = "SAS, Contacto.";

            return View();
        }

        public ActionResult ControllersTest()
        {
            return View();
        }
    }
}
