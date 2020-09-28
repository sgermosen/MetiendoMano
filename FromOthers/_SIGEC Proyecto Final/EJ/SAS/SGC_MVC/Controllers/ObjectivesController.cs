using SGC_MVC.CustomCode;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using System.Data.Entity;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    [IsMenu]
    public class ObjectivesController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();

        //
        // GET: /Objectives/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var documents = db.Documents.Where(dep => dep.companyID == companyID).Include(d => d.Rules).Include(d => d.Status).Where(d => d.DocumentType.name == "Objetivo").ToList();
            ViewBag.ruleID = new SelectList(db.Rules, "ID", "name");
            ViewBag.statusID = new SelectList(db.Status, "ID", "name");
            return View(documents);
        }

        [IsView]
        public ActionResult Details(int id = 0)
        {
            var model = db.Documents
                .Include(d => d.Rules)
                .FirstOrDefault(d => d.ID == id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [IsView]
        public ActionResult Create()
        {
            ObjectivesViewModel model = new ObjectivesViewModel();
            model.allRules = Helper.GetRulesSelect(db);
            ViewBag.documentTypeID = db.DocumentTypes.FirstOrDefault(dt => dt.name == "Objetivo").ID;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ObjectivesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Document d = new Document();
                Helper.updateObjectFields(d);
                d.title = model.title;
                d.documentText = model.document_text;
                d.documentTypeID = db.DocumentTypes.FirstOrDefault(dt => dt.name == "Objetivo").ID;
                d.createUser = WebSecurity.CurrentUserId;
                d.companyID = (int)db.Users.Find(d.createUser).companyID;
                d.version = 1;
                d.url = "";
                d.EDT = 0;
                if (model.selectedRules != null)
                    foreach (int rl in model.selectedRules)
                        d.Rules.Add(db.Rules.Find(rl));
                d.statusID = (int)Helper.StatusTypes.Creacion;

                db.Documents.Add(d);
                db.SaveChanges();

                ViewBag.done = "Datos guardados correctamente.";
            }
            model.allRules = Helper.GetRulesSelect(db);

            return RedirectToAction("Index");
        }

        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Company company = db.Users.Find(WebSecurity.CurrentUserId).Company;
            Document doc = db.Documents.Include(d => d.Rules)
                .FirstOrDefault(d => d.ID == id);
            ObjectivesViewModel model = new ObjectivesViewModel(doc);
            model.allRules = company.Rules
                .ToSelectListItems(t => t.code + " - " + t.name, t => t.ID.ToString(), t => model.selectedRules.Contains(t.ID))
                .ToList();
            ViewBag.documentTypeID = db.DocumentTypes.FirstOrDefault(dt => dt.name == "Objetivo").ID;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ObjectivesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Document d = db.Documents.Include(doc => doc.Rules)
                    .FirstOrDefault(t => model.id == t.ID);

                d.title = model.title;
                d.documentText = model.document_text;
                d.updateDate = DateTime.Now;

                d.Rules.Clear();
                db.SaveChanges();

                if(model.selectedRules != null)
                foreach (int r in model.selectedRules)
                {
                    SGC_MVC.Models.Rule rule = db.Rules.Find(r);
                    d.Rules.Add(rule);
                }

                if (model.submitVal == "nueva version")
                {
                    HistDocument hd = new HistDocument(d);
                    hd.changeReason = model.changeReason;
                    db.HistDocuments.Add(hd);
                    db.SaveChanges();
                    d.version++;
                }

                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.documentTypeID = db.DocumentTypes.FirstOrDefault(dt => dt.name == "Objetivo").ID;
            return View(model);
        }

        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Document d = db.Documents.Find(id);

            if (d == null)
            {
                return HttpNotFound();
            }

            return View(d);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Document d = db.Documents.Find(id);
            d.Rules.Clear();
            db.Documents.Remove(d);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
