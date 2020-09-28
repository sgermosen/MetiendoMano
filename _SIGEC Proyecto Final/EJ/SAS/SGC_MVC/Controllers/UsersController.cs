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
    public class UsersController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Users/
        [IsView]
        public ActionResult Index()
        {
            int userID = WebSecurity.CurrentUserId;
            int? companyID = db.Users.Find(userID).companyID;
            var users = db.Users.Include(u => u.Department)
                .Include(u => u.Position).Include(u => u.User2)
                .Where(usr => usr.companyID == companyID && usr.ID != userID);

            return View(users.ToList());
        }

        //
        // GET: /Users/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        //
        // GET: /Users/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyID = new SelectList(db.Companies, "ID", "name", user.companyID);
            ViewBag.departmentID = new SelectList(db.Departments, "ID", "name", user.departmentID);
            ViewBag.positionID = new SelectList(db.Positions, "ID", "name", user.positionID);
            ViewBag.createUser = new SelectList(db.Users, "ID", "name", user.createUser);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyID = new SelectList(db.Companies, "ID", "name", user.companyID);
            ViewBag.departmentID = new SelectList(db.Departments, "ID", "name", user.departmentID);
            ViewBag.positionID = new SelectList(db.Positions, "ID", "name", user.positionID);
            ViewBag.createUser = new SelectList(db.Users, "ID", "name", user.createUser);
            return View(user);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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