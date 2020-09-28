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
    public class DepartmentController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Department/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var departments = db.Departments.Include(d => d.User).Where(dep => dep.companyID == companyID);
            return View(departments.ToList());
        }

        //
        // GET: /Department/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // GET: /Department/Create
        [IsView]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                department.createUser = WebSecurity.CurrentUserId;
                department.companyID = (int)db.Users.Find(department.createUser).companyID;
                department.createDate = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        //
        // GET: /Department/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                department.updateDate = DateTime.Now;
                department.companyID = (int)db.Users.Find(department.createUser).companyID;
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        //
        // GET: /Department/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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