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
    [IsMenu]
    public class SubMenuController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /SubMenu/
        [IsView]
        public ActionResult Index()
        {
            var submenus = db.SubMenus.Include(s => s.Menu).Include(s => s.View);
            return View(submenus.ToList());
        }

        //
        // GET: /SubMenu/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            SubMenu submenu = db.SubMenus.Find(id);
            if (submenu == null)
            {
                return HttpNotFound();
            }
            return View(submenu);
        }

        //
        // GET: /SubMenu/Create
        [IsView]
        public ActionResult Create()
        {
            ViewBag.menuID = new SelectList(db.Menus, "ID", "name");
            ViewBag.viewID = new SelectList(db.Views, "ID", "name");
            return View();
        }

        //
        // POST: /SubMenu/Create

        [HttpPost]
        public ActionResult Create(SubMenu submenu)
        {
            if (ModelState.IsValid)
            {
                db.SubMenus.Add(submenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.menuID = new SelectList(db.Menus, "ID", "name", submenu.menuID);
            ViewBag.viewID = new SelectList(db.Views, "ID", "name", submenu.viewID);
            return View(submenu);
        }

        //
        // GET: /SubMenu/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            SubMenu submenu = db.SubMenus.Find(id);
            if (submenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.menuID = new SelectList(db.Menus, "ID", "name", submenu.menuID);
            ViewBag.viewID = new SelectList(db.Views, "ID", "name", submenu.viewID);
            return View(submenu);
        }

        //
        // POST: /SubMenu/Edit/5

        [HttpPost]
        public ActionResult Edit(SubMenu submenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.menuID = new SelectList(db.Menus, "ID", "name", submenu.menuID);
            ViewBag.viewID = new SelectList(db.Views, "ID", "name", submenu.viewID);
            return View(submenu);
        }

        //
        // GET: /SubMenu/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            SubMenu submenu = db.SubMenus.Find(id);
            if (submenu == null)
            {
                return HttpNotFound();
            }
            return View(submenu);
        }

        //
        // POST: /SubMenu/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubMenu submenu = db.SubMenus.Find(id);
            db.SubMenus.Remove(submenu);
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