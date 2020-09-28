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

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class ProcessController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Process/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var processes = db.Processes.Include(p => p.User).Include(p => p.ProcessType)
                .Where(dep => dep.companyID == companyID);
            return View(processes.ToList());
        }

        //
        // GET: /Process/Details/5

        [IsView]
        public ActionResult Details(int id = 0)
        {
            Process process = db.Processes.Find(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyLogo = Helper.GetCompanyImage(db);
            ViewBag.histProcesses = db.HistProcesses.Where(hp => hp.processID == id).ToList();
            return View(process);
        }

        //
        // GET: /Process/Create
        [IsView]
        public ActionResult Create()
        {
            //ViewBag.createUser = new SelectList(db.Users, "ID", "name");
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.processTypeID = new SelectList(db.ProcessTypes.Where(pt => pt.companyID == companyID), "ID", "name");
            ViewBag.ruleID = Helper.GetRulesSelect(db);
            ViewBag.responsible = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name");
            return View();
        }

        //
        // POST: /Process/Create

        [HttpPost]
        public ActionResult Create(Process process)
        {
            if (ModelState.IsValid)
            {
                process.statusID = 1;
                process.version = 1;
                process.createUser = WebSecurity.CurrentUserId;
                process.companyID = (int)db.Users.Find(process.createUser).companyID;
                db.Processes.Add(process);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = process.ID });
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.processTypeID = new SelectList(db.ProcessTypes, "ID", "name", process.processTypeID);
            ViewBag.ruleID = new SelectList(db.Rules, "ID", "code", process.ruleID);
            ViewBag.responsible = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", process.responsible);
            return View(process);
        }

        //
        // GET: /Process/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Process process = db.Processes.Include(t => t.Entities).FirstOrDefault(t => t.ID == id);

            if (process == null)
            {
                return HttpNotFound();
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            ViewBag.Proveedorores = new MultiSelectList(db.Entities.Where(dep => dep.companyID == companyID)
                .Where(d => d.entityTypeID == (int)Helper.EntityTypes.Proveedor),
                "ID", "name",
                process.Entities.Where(d => d.entityTypeID == (int)Helper.EntityTypes.Proveedor).Select(m => m.ID));

            ViewBag.Clientes = new MultiSelectList(db.Entities.Where(dep => dep.companyID == companyID)
                .Where(d => d.entityTypeID == (int)Helper.EntityTypes.Cliente),
                "ID", "name",
                process.Entities.Where(d => d.entityTypeID == (int)Helper.EntityTypes.Cliente).Select(m => m.ID));

            var ver = process.Indicators.Select(m => m.ID);

            ViewBag.Indicadores =
                new MultiSelectList(db.Indicators.Where(dep => dep.companyID == companyID),
                    "ID", "name", ver);


            ViewBag.ILProcessResource = db.Subcategories.ToSelectListItems(
               d => d.Category.name + "-" + d.name,
               d => d.ID.ToString(),
               d => process.Subcategories.Contains(db.Subcategories.Find(d.ID)));

            ViewBag.ILProcessDocuments = db.ProcessDocuments.Include(pd => pd.Document).Where(d => d.processID == process.ID).ToList();
            ViewBag.processTypeID = new SelectList(db.ProcessTypes, "ID", "name", process.processTypeID);
            Company c = db.Companies.Include(co => co.Rules).FirstOrDefault(co => co.ID == companyID);
            ViewBag.ruleID = new SelectList(c.Rules, "ID", "code", process.ruleID);
            ViewBag.ILRules = c.Rules.ToSelectListItems(
               d => d.code + "-" + d.name,
               d => d.ID.ToString(),
               d => process.Rules.Contains(db.Rules.Find(d.ID)));
            ViewBag.responsible = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", process.responsible);
            var ruleDoc = c.Rules.FirstOrDefault(cr => cr.ID == process.ruleID);
            var doc = db.Documents.Include(dc => dc.Document1).FirstOrDefault(dc => dc.ID == ruleDoc.documentID);

            if (doc == null || doc.Document1 == null)
            {
                ViewBag.isoRequisites = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                IEnumerable<Document> allDocs = doc.Document1.ToList(), currentList = null;


                foreach (Document d in allDocs.ToList())
                {
                    currentList = allDocs.Union(d.Document1.ToList());
                    allDocs = allDocs.Union(currentList);
                }
                var ireq = process.Documents.Select(pd => pd.ID).ToList();
                ViewBag.isoRequisites = allDocs.ToList().ToSelectListItems(
                    docs => (docs.Document2.EDT == 0) ?
                        docs.EDT.ToString() + ". " + docs.title :
                        (docs.Document2.EDT.ToString() + ".") + docs.EDT.ToString() + ". " + docs.title, docs => docs.ID.ToString(),
                        pd => ireq.Contains(pd.ID));
            }

            return View(process);
        }

        //
        // POST: /Process/Edit/5

        [HttpPost]
        public ActionResult Edit
            (
                Process process, int[] Proveedorores, int[] Clientes, int[] ILProcessResource,
                int[] ILRules, int[] Indicadores, int[] isoRequisites, string submitVal
            )
        {
            if (ModelState.IsValid)
            {
                process.updateDate = DateTime.Now;
                db.Entry(process).State = EntityState.Modified;

                var proc = db.Processes.Include(d => d.Entities).Include(pd => pd.Documents).Include(d => d.Subcategories).Include(d => d.Rules).FirstOrDefault(d => d.ID == process.ID);
                proc.Entities.Clear();
                proc.Subcategories.Clear();
                proc.Rules.Clear();
                proc.Documents.Clear();

                db.SaveChanges();

                if (Proveedorores != null)
                    foreach (int entity in Proveedorores)
                    {
                        process.Entities.Add(db.Entities.Find(entity));
                    }

                if (Clientes != null)
                    foreach (int entity in Clientes)
                    {
                        process.Entities.Add(db.Entities.Find(entity));
                    }

                if (ILProcessResource != null)
                    foreach (int entity in ILProcessResource)
                    {
                        process.Subcategories.Add(db.Subcategories.Find(entity));
                    }
                if (ILRules != null)
                    foreach (int entity in ILRules)
                    {
                        process.Rules.Add(db.Rules.Find(entity));
                    }

                if (Indicadores != null)
                    foreach (int indic in Indicadores)
                    {
                        proc.Indicators.Clear();
                        process.Indicators.Add(db.Indicators
                            .FirstOrDefault(i => i.ID == indic));
                    }

                if (isoRequisites != null)
                {
                    foreach (int ir in isoRequisites)
                    {
                        process.Documents.Add(db.Documents.Find(ir));
                    }
                }

                if (submitVal == "nueva version")
                {
                    HistProcess hp = new HistProcess(process);
                    hp.changeReason = process.changeReason;
                    process.version++;
                    db.HistProcesses.Add(hp);
                }

                db.SaveChanges();
                if (submitVal == "Asociar Archivo")
                {
                    return RedirectToAction("AssociatedDocuments", new { id = process.ID });
                }
                else
                    return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            ViewBag.Proveedorores = new MultiSelectList(db.Entities.Where(dep => dep.companyID == companyID)
                .Where(d => d.entityTypeID == (int)Helper.EntityTypes.Proveedor),
                "ID", "name",
                process.Entities.Where(d => d.entityTypeID == (int)Helper.EntityTypes.Proveedor).Select(m => m.ID));

            ViewBag.Clientes = new MultiSelectList(db.Entities.Where(dep => dep.companyID == companyID)
                .Where(d => d.entityTypeID == (int)Helper.EntityTypes.Cliente),
                "ID", "name",
                process.Entities.Where(d => d.entityTypeID == (int)Helper.EntityTypes.Cliente).Select(m => m.ID));

            var ver = process.Indicators.Select(m => m.ID);

            ViewBag.Indicadores =
                new MultiSelectList(db.Indicators.Where(dep => dep.companyID == companyID),
                    "ID", "name", ver);


            ViewBag.ILProcessResource = db.Subcategories.ToSelectListItems(
               d => d.Category.name + "-" + d.name,
               d => d.ID.ToString(),
               d => process.Subcategories.Contains(db.Subcategories.Find(d.ID)));

            ViewBag.ILProcessDocuments = db.ProcessDocuments.Include(pd => pd.Document).Where(d => d.processID == process.ID).ToList();
            ViewBag.processTypeID = new SelectList(db.ProcessTypes, "ID", "name", process.processTypeID);
            Company c = db.Companies.Include(co => co.Rules).FirstOrDefault(co => co.ID == companyID);
            ViewBag.ruleID = new SelectList(c.Rules, "ID", "code", process.ruleID);
            ViewBag.ILRules = c.Rules.ToSelectListItems(
               d => d.code + "-" + d.name,
               d => d.ID.ToString(),
               d => process.Rules.Contains(db.Rules.Find(d.ID)));
            ViewBag.responsible = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", process.responsible);
            var ruleDoc = c.Rules.FirstOrDefault(cr => cr.ID == process.ruleID);
            var doc = db.Documents.Include(dc => dc.Document1).FirstOrDefault(dc => dc.ID == ruleDoc.documentID);

            if (doc == null || doc.Document1 == null)
            {
                ViewBag.isoRequisites = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                IEnumerable<Document> allDocs = doc.Document1.ToList(), currentList = null;

                foreach (Document d in allDocs.ToList())
                {
                    currentList = allDocs.Union(d.Document1.ToList());
                    allDocs = allDocs.Union(currentList);
                }
                var ireq = process.Documents.Select(pd => pd.ID).ToList();
                ViewBag.isoRequisites = allDocs.ToList().ToSelectListItems(
                    docs => (docs.Document2.EDT == 0) ?
                        docs.EDT.ToString() + ". " + docs.title :
                        (docs.Document2.EDT.ToString() + ".") + docs.EDT.ToString() + ". " + docs.title, docs => docs.ID.ToString(),
                        pd => ireq.Contains(pd.ID));
            }
            return View(process);
        }

        [IsView]
        public ActionResult AssociatedDocuments(int id)
        {
            ProcessDocument model = new ProcessDocument();
            model.processID = id;
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentID = new SelectList(db.Documents.Where(d => d.companyID == companyID), "ID", "title");
            return View(model);
        }

        [HttpPost]
        public ActionResult AssociatedDocuments(ProcessDocument model, bool isUnlinked)
        {
            if (ModelState.IsValid)
            {
                if (isUnlinked)
                {
                    model.documentID = null;
                }
                else
                {
                    model.text = null;
                }
                db.ProcessDocuments.Add(model);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.processID });
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentID = new SelectList(db.Documents.Where(d => d.companyID == companyID), "ID", "title");
            return View(model);
        }

        [IsView]
        public ActionResult EditAssociatedDocument(int id = 0)
        {
            var model = db.ProcessDocuments.Include(pd => pd.Document)
                .FirstOrDefault(pd => pd.ID == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentID = new SelectList(db.Documents.Where(d => d.companyID == companyID), "ID", "title");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAssociatedDocument(ProcessDocument model, bool isUnlinked)
        {
            if (ModelState.IsValid)
            {
                if (isUnlinked)
                {
                    model.documentID = null;
                }
                else
                {
                    model.text = null;
                }
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = model.processID });
            }
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.documentID = new SelectList(db.Documents.Where(d => d.companyID == companyID), "ID", "title");

            return View(model);
        }

        //
        // GET: /Process/Delete/5
        [IsView]
        public ActionResult DeleteAssociatedDocuments(int id = 0)
        {
            ProcessDocument process = db.ProcessDocuments.Find(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        [HttpPost, ActionName("DeleteAssociatedDocuments")]
        public ActionResult DeleteAssociatedDocumentsConfirmed(int id)
        {
            ProcessDocument pdoc = db.ProcessDocuments.Find(id);
            int processID = pdoc.processID;
            db.ProcessDocuments.Remove(pdoc);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = processID });
        }


        public ActionResult UpdateLevel2(int ID, int ruleID)
        {
            var process = db.Processes.Find(ID);
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.Find(companyID);

            var ruleDoc = c.Rules.FirstOrDefault(cr => cr.ID == ruleID);
            var doc = db.Documents.Include(dc => dc.Document1).FirstOrDefault(dc => dc.ID == ruleDoc.documentID);

            if (doc == null || doc.Document1 == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<Document> allDocs = doc.Document1.ToList(), currentList = null;

            foreach (Document d in allDocs.ToList())
            {
                currentList = allDocs.Union(d.Document1.ToList());
                allDocs = allDocs.Union(currentList);
            }
            var ireq = process.Documents.Select(pd => pd.ID).ToList();
            var isoRequisites = allDocs.ToList().ToSelectListItems(
                docs => (docs.Document2.EDT == 0) ?
                    docs.EDT.ToString() + ". " + docs.title :
                    (docs.Document2.EDT.ToString() + ".") + docs.EDT.ToString() + ". " + docs.title, docs => docs.ID.ToString(),
                    pd => ireq.Contains(pd.ID));

            return Json(isoRequisites, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Process/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Process process = db.Processes.Find(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        //
        // POST: /Process/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Process process = db.Processes.Find(id);
            process.Entities.Clear();
            process.Indicators.Clear();
            process.ProcessDocuments.Clear();
            process.Rules.Clear();
            db.Processes.Remove(process);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}