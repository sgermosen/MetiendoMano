using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SGC_MVC.Models.ViewModels;
using SGC_MVC.CustomCode;
using System.Data;
using WebMatrix.WebData;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    [IsMenu]
    public class ManualController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();
        //
        // GET: /Manual/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var policies = db.Documents.Include(d => d.Rules).Include(d => d.Status)
                .Where(d => d.DocumentType.name == "Manual" && d.documentParentID == null && d.companyID == companyID);

            return View(policies.ToList());
        }

        [IsView]
        public ActionResult Details(int id = 0)
        {
            var manual = db.Documents.Find(id);

            if (manual == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyLogo = Helper.GetCompanyImage(db);
            ViewBag.HistDocuments = db.HistDocuments.Where(hds => hds.documentID == id).ToList();

            return View(manual);
        }

        [IsView]
        public ActionResult Create()
        {
            DocumentEditViewModel model = new DocumentEditViewModel();
            model.document.documentTypeID = (
                        from d in db.DocumentTypes
                        where d.name == "Manual"
                        select d.ID
                    ).FirstOrDefault();
            ViewBag.allRules = Helper.GetRulesSelect(db);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DocumentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.document.version = 1;
                model.document.createUser = WebSecurity.CurrentUserId;
                model.document.statusID = 1;
                model.document.EDT = 1;
                model.document.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
                Helper.updateObjectFields(model.document);

                if (model.selectedRules != null)
                    foreach (int ruleId in model.selectedRules)
                    {
                        model.document.Rules.Add(db.Rules.Find(ruleId));
                    }

                db.Documents.Add(model.document);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.document.ID });
            }

            ViewBag.allRules = Helper.GetRulesSelect(db);
            return View(model);
        }

        [IsView]
        public ActionResult Edit(int id = 0)
        {
            DocumentEditViewModel model = new DocumentEditViewModel();
            model.document = db.Documents.Include(d => d.Rules).Include(d => d.Status)
                .Include(d => d.Document1).Include(d => d.Document2).FirstOrDefault(d => d.ID == id);

            ViewBag.allRules = Helper.GetRulesSelect(db);
            model.selectedRules = (from r in model.document.Rules
                                   select r.ID).ToArray();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DocumentEditViewModel model, string submitVal)
        {
            if (ModelState.IsValid)
            {
                Helper.updateObjectFields(model.document);
                model.document.Rules.Clear();
                model.document.updateDate = DateTime.Now;
                model.document.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;

                if (submitVal == "nueva version")
                {
                    HistDocument d = new HistDocument(model.document);
                    d.changeReason = model.changeReason;
                    db.HistDocuments.Add(d);
                    db.SaveChanges();
                    model.document.version++;
                }

                db.Entry(model.document).State = EntityState.Modified;
                db.SaveChanges();

                Document document = db.Documents.Include(d => d.Rules)
                    .FirstOrDefault(d => d.ID == model.document.ID);
                document.Rules.Clear();
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();

                if (model.selectedRules != null)
                    foreach (int ruleId in model.selectedRules)
                    {
                        document.Rules.Add(db.Rules.Find(ruleId));
                    }

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();

                if (submitVal == "Capitulo")
                {
                    return RedirectToAction("CreateChild", new { id = model.document.ID });
                }
                else
                    return RedirectToAction("Index");
            }
            ViewBag.allRules = Helper.GetRulesSelect(db);

            return View(model);
        }

        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Document manual = db.Documents.Include(d => d.Document1)
                .FirstOrDefault(d => d.ID == id);
            if (manual == null)
            {
                return HttpNotFound();
            }

            return View(manual);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var doc = db.Documents.Include(d => d.Document1).Include(d => d.Rules)
                .FirstOrDefault(d => d.ID == id);

            doc.Rules.Clear();
            if (doc.Document1 != null)
                foreach (Document d in doc.Document1.ToList())
                {
                    db.Documents.Remove(d);
                }
            doc.Document1.Clear();
            foreach (Document docy in doc.Document1)
            {
                if (docy.documentParentID == doc.documentParentID)
                {
                    docy.documentParentID = null;
                }
            }

            db.Documents.Remove(doc);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [IsView]
        public ActionResult CreateChild(int id = 0)
        {
            Document parent = db.Documents.Include(d => d.Rules)
                .FirstOrDefault(d => d.ID == id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            SubDocumentEditViewModel model = new SubDocumentEditViewModel();
            model.document.Document2 = parent;
            model.document.documentParentID = id;
            model.document.documentTypeID = parent.documentTypeID;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateChild(SubDocumentEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Document parent = db.Documents.Include(d => d.Rules)
                    .Include(d => d.Document1).Include(d => d.Document2)
                    .FirstOrDefault(d => d.ID == model.document.documentParentID);
                model.document.documentTypeID = parent.documentTypeID;
                model.document.companyID = parent.companyID;
                Helper.updateObjectFields(model.document);
                double edt = 0;
                if (parent.Document1.Count() > 0)
                {
                    edt = (from d in parent.Document1
                           select d.EDT).Max();
                }
                model.document.EDT = edt + 1;

                db.Documents.Add(model.document);
                db.SaveChanges();

                if (parent.Document2 != null)
                {
                    return RedirectToAction("EditChild", new { id = parent.ID });
                }
                else
                {
                    return RedirectToAction("Edit", new { id = parent.ID });
                }
            }

            return View(model);
        }

        [IsView]
        public ActionResult EditChild(int id = 0)
        {
            SubDocumentEditViewModel model = new SubDocumentEditViewModel();
            model.document = db.Documents.Include(d => d.Rules).FirstOrDefault(d => d.ID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditChild(SubDocumentEditViewModel model, string submitVal)
        {
            if (ModelState.IsValid)
            {
                model.document.updateDate = DateTime.Now;
                model.document.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
                Helper.updateObjectFields(model.document);
                model.document.Rules.Clear();
                db.Entry(model.document).State = EntityState.Modified;
                db.SaveChanges();

                Document document = db.Documents.Include(d => d.Rules)
                    .FirstOrDefault(d => d.ID == model.document.ID);
                document.Rules.Clear();
                db.SaveChanges();

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                if (submitVal == "Capitulo")
                {
                    return RedirectToAction("CreateChild", new { id = model.document.ID });
                }
                else
                {
                    if (db.Documents.Include(d => d.Document2)
                        .FirstOrDefault(d => d.ID == model.document.documentParentID)
                        .Document2 == null)
                    {
                        return RedirectToAction("Edit", new { id = model.document.documentParentID });
                    }
                    return RedirectToAction("EditChild", new { id = model.document.documentParentID });
                }

            }

            model.document.documentTypeID = (from d in db.DocumentTypes
                                             where d.name == "Manual"
                                             select d.ID).FirstOrDefault();

            ViewBag.allRules = Helper.GetRulesSelect(db);

            return View(model);
        }

        [IsView]
        public ActionResult DeleteChild(int id = 0)
        {
            Document doc = db.Documents.Include(d => d.Document2)
                .FirstOrDefault(d => d.ID == id);

            if (doc == null)
            {
                return HttpNotFound();
            }

            return View(doc);
        }

        [HttpPost, ActionName("DeleteChild")]
        public ActionResult DeleteChildConfirmed(int id)
        {
            Document doc = db.Documents.Include(d => d.Document2)
                .Include(d => d.Document1).FirstOrDefault(d => d.ID == id);
            int parent_id = doc.documentParentID.Value;

            if (doc.Document1 != null)
                foreach (Document d in doc.Document1.ToList())
                {
                    d.Rules.Clear();
                    db.Documents.Remove(d);
                }
            doc.Document1.Clear();
            doc.Rules.Clear();
            string action = (doc.Document2.Document2 == null)
                ? "Edit" : "EditChild";
            string query = "update document set EDT = EDT - 1"
                            + "where documentParentID = {0}"
                            + " and EDT > {1};";
            db.Database.ExecuteSqlCommand(query, doc.documentParentID, doc.EDT);
            db.Documents.Remove(doc);
            db.SaveChanges();

            return RedirectToAction(action, new { id = parent_id });
        }

        /// <summary>
        /// Actualiza el orden de los capitulos usando el EDT
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="id"></param>
        /// <param name="fromPosition"></param>
        /// <param name="toPosition"></param>
        /// <param name="direction"></param>
        public void UpdateOrder(int parentID, int id, int fromPosition, int toPosition, string direction)
        {

            if (direction == "back")
            {
                var movedDocuments = db.Documents
                            .Where(d => (toPosition <= d.EDT && d.EDT <= fromPosition)
                                   && d.documentParentID == parentID)
                            .ToList();

                foreach (var document in movedDocuments)
                {
                    document.EDT++;
                    db.Entry(document).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                var movedCompanies = db.Documents
                            .Where(d => (fromPosition <= d.EDT && d.EDT <= toPosition))
                            .ToList();
                foreach (var company in movedCompanies)
                {
                    company.EDT--;
                }
            }

            var doc = db.Documents.FirstOrDefault(d => d.ID == id);
            doc.EDT = toPosition;
            db.Entry(doc).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}