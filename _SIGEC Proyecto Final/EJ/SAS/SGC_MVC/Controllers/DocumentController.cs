using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using WebMatrix.WebData;
using SGC_MVC.CustomCode;
using SGC_MVC.Models.ViewModels;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    [IsMenu]
    public class DocumentController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Document/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var documents = db.Documents.Include(d => d.DocumentType).Include(d => d.Rules).Where(dep => dep.companyID == companyID && dep.documentParentID == null);
            return View(documents.ToList());
        }

        //
        // GET: /Document/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // GET: /Document/Create
        [IsView]
        public ActionResult Create()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentTypeID = new SelectList(db.DocumentTypes, "ID", "name");
            ViewBag.documentParentID = new SelectList(db.Documents.Where(dep => dep.companyID == companyID), "ID", "title");
            ViewBag.createUser = new SelectList(db.Users, "ID", "name");
            ViewBag.allRules = Helper.GetRulesSelect(db);
            return View();
        }

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult Create(DocumentCreateViewModel model, int[] selectedRules)
        {
            Document document = new Document();
            if (ModelState.IsValid)
            {
                model.SetValues(document);
                Helper.updateObjectFields(document);
                document.createUser = WebSecurity.CurrentUserId;
                document.companyID = (int)db.Users.Find(document.createUser).companyID;
                document.statusID = 1;
                if (selectedRules != null)
                    foreach (int rl in selectedRules)
                    {
                        document.Rules.Add(db.Rules.Find(rl));
                    }
                document.EDT = 1;
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentTypeID = new SelectList(db.DocumentTypes, "ID", "name", document.documentTypeID);
            ViewBag.documentParentID = new SelectList(db.Documents.Where(dep => dep.companyID == companyID), "ID", "title", document.documentParentID);
            ViewBag.createUser = new SelectList(db.Users, "ID", "name", document.createUser);
            ViewBag.allRules = Helper.GetRulesSelect(db);
            return View(document);
        }

        //
        // GET: /Document/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Document document = db.Documents.Include(d => d.Rules)
                .FirstOrDefault(d => d.ID == id);
            if (document == null)
            {
                return HttpNotFound();
            }
            DocumentInnerViewModel model = new DocumentInnerViewModel();

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name");

            var selectedRules = document.Rules.Select(d => d.ID);
            ViewBag.documentTypeID = db.DocumentTypes
                .ToSelectListItems(
                t => t.name, t => t.ID.ToString(),
                t => t.ID == document.documentTypeID);
            ViewBag.documentParentID = db.Documents.ToSelectListItems(
                d => d.title,
                d => d.ID.ToString(),
                d => d.ID == document.documentParentID);
            ViewBag.create_user = new SelectList(db.Users, "ID", "name", document.createUser);
            ViewBag.allRules = Helper.GetRulesSelect(db);
            model.document = document;
            model.selectedRules = selectedRules.ToArray();
            return View(model);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentInnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string text = model.document.documentText;
                string url = model.document.url;
                model.document.documentText = (string.IsNullOrEmpty(text)) ? "" : text;
                model.document.url = (string.IsNullOrEmpty(url)) ? "" : url;
                model.document.updateDate = DateTime.Now;
                model.document.companyID = (int)db.Users.Find(model.document.createUser).companyID;
                db.Entry(model.document).State = EntityState.Modified;
                db.SaveChanges();

                Document document = db.Documents.Include(d => d.Rules).FirstOrDefault(d => d.ID == model.document.ID);
                document.Rules.Clear();
                db.SaveChanges();
                if (model.selectedRules != null)
                    foreach (int ruleId in model.selectedRules)
                    {
                        var rule = db.Rules.Find(ruleId);

                        document.Rules.Add(rule);
                        db.Entry(document).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                return RedirectToAction("Index");
            }

            ViewBag.documentTypeID = new SelectList(db.DocumentTypes, "ID", "name", model.document.documentTypeID);
            ViewBag.documentParentID = new SelectList(db.Documents, "ID", "title", model.document.documentTypeID);
            ViewBag.create_user = new SelectList(db.Users, "ID", "name", model.document.createUser);
            ViewBag.allRules = Helper.GetRulesSelect(db);
            return View(model);
        }

        //
        // GET: /Document/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Document/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Document document = db.Documents.Find(id);
                document.Rules.Clear();
                db.Documents.Remove(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error", null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}