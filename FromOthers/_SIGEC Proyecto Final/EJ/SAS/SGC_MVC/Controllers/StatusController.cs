using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using WebMatrix.WebData;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    public class StatusController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Status/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var status = db.Status.Where(dep => dep.companyID == companyID || dep.companyID == null).Include(s => s.User);
            return View(status.ToList());
        }

        //
        // GET: /Status/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        //
        // GET: /Status/Create
        [IsView]
        public ActionResult Create()
        {
            ViewBag.createUser = new SelectList(db.Users, "ID", "name");
            return View();
        }

        //
        // POST: /Status/Create

        [HttpPost]
        public ActionResult Create(Status status)
        {

            status.createUser = WebSecurity.CurrentUserId;
            //status.create_date = DateTime.Now;
            status.companyID= db.Users.Find(WebSecurity.CurrentUserId).companyID;
            if (ModelState.IsValid)
            {
                db.Status.Add(status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.create_user = new SelectList(db.Users, "ID", "name", status.createUser);
            return View(status);
        }

        //
        // GET: /Status/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            ViewBag.create_user = new SelectList(db.Users, "ID", "name", status.createUser);
            return View(status);
        }

        //
        // POST: /Status/Edit/5

        [HttpPost]
        public ActionResult Edit(Status status)
        {
            status.updateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.create_user = new SelectList(db.Users, "ID", "name", status.createUser);
            return View(status);
        }

        //
        // GET: /Status/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        //
        // POST: /Status/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Status status = db.Status.Find(id);
            db.Status.Remove(status);
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