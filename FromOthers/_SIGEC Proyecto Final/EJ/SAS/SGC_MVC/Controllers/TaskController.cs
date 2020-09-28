using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using SGC_MVC.CustomCode;
using WebMatrix.WebData;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class TaskController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Task/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var usertasks = db.UserTasks.Where(dep => dep.companyID == companyID).Include(u => u.Rule)
                .Include(u => u.Status).Include(u => u.User)
                .Include(u => u.User1).Include(u => u.UserTask2)
                .Where(u => u.parentTaskID == null);
            return View(usertasks.ToList());
        }

        //
        // GET: /Task/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            UserTask usertask = db.UserTasks.Find(id);
            if (usertask == null)
            {
                return HttpNotFound();
            }
            return View(usertask);
        }

        //
        // GET: /Task/Create
        [IsView]
        public ActionResult Create()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name");
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name");
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(UserTask usertask, string chainTask = null)
        {
            if (ModelState.IsValid)
            {
                usertask.createUser = WebSecurity.CurrentUserId;
                usertask.companyID = (int)db.Users.Find(usertask.createUser).companyID;
                usertask.statusID = (int)Helper.StatusTypes.Creacion;
                db.UserTasks.Add(usertask);
                db.SaveChanges();
                if (chainTask != null)
                {
                    return RedirectToAction("CreateChildTask", new { taskID = usertask.ID });
                }
                return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;



            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));

            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", usertask.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", usertask.responsible);
            return View(usertask);
        }

        [IsView]
        public ActionResult CreateChildTask(int taskID)
        {
            UserTask model = new UserTask();
            model.parentTaskID = taskID;
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name");
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name");
            model.UserTask2 = db.UserTasks.Find(taskID);
            model.companyID = model.UserTask2.companyID;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateChildTask(UserTask model, string chainTask = null)
        {
            if (ModelState.IsValid)
            {
                model.createUser = WebSecurity.CurrentUserId;
                model.statusID = (int)Helper.StatusTypes.Creacion;
                db.UserTasks.Add(model);
                db.SaveChanges();

                if (chainTask != null)
                {
                    return RedirectToAction("CreateChildTask", new { taskID = model.ID });
                }

                return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", model.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", model.responsible);
            return View(model);
        }


        public ActionResult EditChildTask(int id = 0)
        {
            UserTask model = db.UserTasks.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", model.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", model.responsible);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditChildTask(UserTask usertask, string chainTask = null)
        {
            if (ModelState.IsValid)
            {
                usertask.updateDate = DateTime.Now;
                db.Entry(usertask).State = EntityState.Modified;
                db.SaveChanges();

                if (chainTask != null)
                {
                    return RedirectToAction("CreateChildTask", new { taskID = usertask.ID });
                }
                return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", usertask.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", usertask.responsible);
            ViewBag.parentTaskID = new SelectList(db.UserTasks, "ID", "name", usertask.parentTaskID);

            return View(usertask);
        }

        //
        // GET: /Task/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            UserTask usertask = db.UserTasks.Find(id);
            if (usertask == null)
            {
                return HttpNotFound();
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", usertask.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", usertask.responsible);
            ViewBag.parentTaskID = new SelectList(db.UserTasks.Where(dep => dep.companyID == companyID), "ID", "name", usertask.parentTaskID);
            return View(usertask);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(UserTask usertask, string chainTask = null)
        {
            if (ModelState.IsValid)
            {
                usertask.updateDate = DateTime.Now;
                db.Entry(usertask).State = EntityState.Modified;
                db.SaveChanges();

                if (chainTask != null)
                {
                    return RedirectToAction("CreateChildTask", new { taskID = usertask.ID });
                }
                return RedirectToAction("Index");
            }

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            Company companyIDd = db.Companies.FirstOrDefault((d => d.ID == companyID));
            ViewBag.ruleID = new SelectList(companyIDd.Rules.ToList(), "ID", "name", usertask.ruleID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID), "ID", "name", usertask.responsible);
            ViewBag.parentTaskID = new SelectList(db.UserTasks.Where(dep => dep.companyID == companyID), "ID", "name", usertask.parentTaskID);
            return View(usertask);
        }

        //
        // GET: /Task/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            UserTask usertask = db.UserTasks.Find(id);
            if (usertask == null)
            {
                return HttpNotFound();
            }
            return View(usertask);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTask usertask = db.UserTasks.Find(id);
            db.UserTasks.Remove(usertask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteChildTask(int id = 0)
        {
            UserTask model = db.UserTasks.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("DeleteChildTask")]
        public ActionResult DeleteChildTaskConfirmed(int id)
        {
            UserTask model = db.UserTasks.Include(m => m.UserTask2)
                .FirstOrDefault(m => m.ID == id);
            string action = (model.UserTask2.UserTask2 == null) ?
                "Edit" : "EditChildTask";
            int parentID = model.parentTaskID.Value;

            db.UserTasks.Remove(model);
            db.SaveChanges();

            return RedirectToAction(action, new { id = parentID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}