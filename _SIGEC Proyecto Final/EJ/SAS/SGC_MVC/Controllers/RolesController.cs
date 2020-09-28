using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    public class RolesController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Roles/
        [IsView]
        public ActionResult Index()
        {
            return View(db.Webpages_Roles.ToList());
        }

        //
        // GET: /Roles/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Webpages_Roles webpages_roles = db.Webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // GET: /Roles/Create
        [IsView]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create

        [HttpPost]
        public ActionResult Create(Webpages_Roles webpages_roles)
        {
            if (ModelState.IsValid)
            {
                db.Webpages_Roles.Add(webpages_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webpages_roles);
        }

        //
        // GET: /Roles/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Webpages_Roles webpages_roles = db.Webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // POST: /Roles/Edit/5

        [HttpPost]
        public ActionResult Edit(Webpages_Roles webpages_roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webpages_roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webpages_roles);
        }

        //
        // GET: /Roles/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Webpages_Roles webpages_roles = db.Webpages_Roles.Find(id);
            if (webpages_roles == null)
            {
                return HttpNotFound();
            }
            return View(webpages_roles);
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Webpages_Roles webpages_roles = db.Webpages_Roles.Find(id);
            db.Webpages_Roles.Remove(webpages_roles);
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