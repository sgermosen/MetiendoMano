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
    public class InstructionTasksController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /InstructionTasks/Create
        [IsView]
        public ActionResult Create(int ID, int ruleID, int processID )
        {
            Task task = new Task();
            task.workID = ID;
            task.ruleID = ruleID;
            task.processID = processID;

            return View(task);
        }

        //
        // POST: /InstructionTasks/Create

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.createUser = WebSecurity.CurrentUserId;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Edit", "InstructionWorks", new { id = task.workID });
            }

            return View(task);
        }

        //
        // GET: /InstructionTasks/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }

        //
        // POST: /InstructionTasks/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                task.updateDate = DateTime.Now;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "InstructionWorks", new { id = task.workID });
            }

            return View(task);
        }

        //
        // GET: /InstructionTasks/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /InstructionTasks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Edit", "InstructionWorks", new { id = task.workID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}