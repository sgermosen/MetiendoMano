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
    [CustomAuthorize]
    [IsMenu]
    public class DocTypeController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /DocType/
        [IsView]
        public ActionResult Index()
        {
            var document_type = db.DocumentTypes.Include(d => d.User);
            return View(document_type.ToList());
        }

        //
        // GET: /DocType/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            DocumentType document_type = db.DocumentTypes.Find(id);
            if (document_type == null)
            {
                return HttpNotFound();
            }
            return View(document_type);
        }

        //
        // GET: /DocType/Create
        [IsView]
        public ActionResult Create()
        {
            ViewBag.create_user = new SelectList(db.Users, "user_id", "name");
            return View();
        }

        //
        // POST: /DocType/Create

        [HttpPost]
        public ActionResult Create(DocumentType document_type)
        {
            if (ModelState.IsValid)
            {
                document_type.createUser = WebSecurity.CurrentUserId;
                db.DocumentTypes.Add(document_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.create_user = new SelectList(db.Users, "ID", "name", document_type.createUser);
            return View(document_type);
        }

        //
        // GET: /DocType/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            DocumentType document_type = db.DocumentTypes.Find(id);
            if (document_type == null)
            {
                return HttpNotFound();
            }
            return View(document_type);
        }

        //
        // POST: /DocType/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentType document_type)
        {
            if (ModelState.IsValid)
            {
                document_type.updateDate = DateTime.Now;
                db.Entry(document_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document_type);
        }

        //
        // GET: /DocType/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            DocumentType document_type = db.DocumentTypes.Find(id);
            if (document_type == null)
            {
                return HttpNotFound();
            }
            return View(document_type);
        }

        //
        // POST: /DocType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType document_type = db.DocumentTypes.Find(id);
            db.DocumentTypes.Remove(document_type);
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