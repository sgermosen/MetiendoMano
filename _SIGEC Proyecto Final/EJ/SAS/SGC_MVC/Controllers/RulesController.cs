using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using WebMatrix.WebData;
using SGC_MVC.Models.ViewModels;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    public class RulesController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Rules/
        [IsView]
        public ActionResult Index()
        {
            return View(db.Rules.ToList());
        }

        //
        // GET: /Rules/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            SGC_MVC.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        //
        // GET: /Rules/Create
        [IsView]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rules/Create

        [HttpPost]
        public ActionResult Create(SGC_MVC.Models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                rule.createUser = WebSecurity.CurrentUserId;
                db.Rules.Add(rule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rule);
        }

        //
        // GET: /Rules/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            SGC_MVC.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        //
        // POST: /Rules/Edit/5

        [HttpPost]
        public ActionResult Edit(SGC_MVC.Models.Rule rule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rule);
        }

        //
        // GET: /Rules/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            SGC_MVC.Models.Rule rule = db.Rules.Find(id);
            if (rule == null)
            {
                return HttpNotFound();
            }
            return View(rule);
        }

        //
        // POST: /Rules/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SGC_MVC.Models.Rule rule = db.Rules.Find(id);
            db.Rules.Remove(rule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RulesList()
        {
            return View();
        }

        public ActionResult TableRequest(JQueryDataTableParamModel model)
        {

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));

            var rules = db.Rules.Where(d=>d.Companies.Contains(companyIDd));
            IEnumerable<SGC_MVC.Models.Rule> filteredRules;
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                //determinar si se puede buscar por dichos parametros
                var isCodeSearchable = Convert.ToBoolean(Request["bSearchable_0"]);
                var isNameSearchable = Convert.ToBoolean(Request["bSearchable_1"]);

                filteredRules = rules.Where(r => isCodeSearchable && r.code.ToLower().Contains(model.sSearch.ToLower())
                        || isNameSearchable && r.name.ToLower().Contains(model.sSearch.ToLower()));
            }
            else
            {
                filteredRules = rules;
            }

            var isCodeSortable = Convert.ToBoolean(Request["bSortable_0"]);
            var isNameSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<SGC_MVC.Models.Rule, string> orderingFunction = 
                (c => sortColumnIndex == 0 && isCodeSortable ? c.code :
                    sortColumnIndex == 1 && isNameSortable ? c.name :
                "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc

            if (sortDirection == "asc")
                filteredRules = filteredRules.OrderBy(orderingFunction);
            else
                filteredRules = filteredRules.OrderByDescending(orderingFunction);

            var displayedRules = filteredRules.Skip(model.iDisplayStart).Take(model.iDisplayLength); ;
            var result = from r in filteredRules
                         select new[] { r.code, r.name };

            return Json(new
                {
                    sEcho = model.sEcho,
                    iTotalRecords = rules.Count(),
                    iTotalDisplayRecords = filteredRules.Count(),
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}