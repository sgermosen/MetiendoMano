using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using System.Data.Entity;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class InstructionWorksController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();
        //
        // GET: /InstructionWorks/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var instWorks = db.InstructionWorks
                .Include(t => t.Department)
                .Include(t => t.Rule)
                .Include(t => t.Process).Where(dep => dep.companyID == companyID).ToList();

            return View(instWorks);
        }

        //
        // GET: /InstructionWorks/Details/5
        [IsView]
        public ActionResult Details(int id)
        {
            var instWork = db.InstructionWorks
                                .Include(w => w.Department)
                                .Include(w => w.Rule)
                                .Include(w => w.Process)
                                .FirstOrDefault(w => w.ID == id);

            return View(instWork);
        }

        //
        // GET: /InstructionWorks/Create
        [IsView]
        public ActionResult Create()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.ruleID = Helper.GetRulesSelect(db);
            ViewBag.processID = new SelectList(db.Processes.Where(dep => dep.companyID == companyID), "ID", "name");
            ViewBag.departmentID = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name");
            ViewBag.responsible = new SelectList(db.Users, "ID", "name");

            InstructionWork model = new InstructionWork();

            return View(model);
        }

        //
        // POST: /InstructionWorks/Create

        [HttpPost]
        public ActionResult Create(InstructionWork model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.statusID = (int)Helper.StatusTypes.Creacion;
                    model.createUser = WebSecurity.CurrentUserId;
                    model.companyID = (int)db.Users.Find(model.createUser).companyID;
                    db.InstructionWorks.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Edit", new { id = model.ID });
                }


                return View(model);
            }
            catch
            {
                return View("Error", null);
            }
        }

        //
        // GET: /InstructionWorks/Edit/5
        [IsView]
        public ActionResult Edit(int id)
        {
            var instWork = db.InstructionWorks.Include(d => d.Department)
                                     .Include(p => p.Process)
                                     .Include(u => u.User)
                                     .FirstOrDefault(iw => iw.ID == id);

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            ViewBag.ruleID = new SelectList(db.Rules, "ID", "name", instWork.ruleID);
            ViewBag.processID = new SelectList(db.Processes.Where(dep => dep.companyID == companyID), "ID", "name", instWork.processID);
            ViewBag.departmentID = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name", instWork.departmentID);
            ViewBag.responsible = new SelectList(db.Users, "ID", "name", instWork.responsible);

            return View(instWork);
        }

        //
        // POST: /InstructionWorks/Edit/5

        [HttpPost]
        public ActionResult Edit(InstructionWork model, string submitVal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (submitVal == "nueva version")
                    {
                        HistInstructionWork histInstWork = new HistInstructionWork(model);
                        histInstWork.changeReason = model.changeReason;

                        db.HistInstructionWorks.Add(histInstWork);
                        db.SaveChanges();
                    }
                    model.version++;
                    model.updateDate = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch
            {
                return View("Error", null);
            }
        }

        //
        // GET: /InstructionWorks/Delete/5
        [IsView]
        public ActionResult Delete(int id)
        {
            InstructionWork model = db.InstructionWorks
                                        .Include(m => m.Process)
                                        .Include(m => m.Rule)
                                        .Include(m => m.User)
                                        .Include(m => m.Department)
                                        .FirstOrDefault(m => m.ID == id);

            return View(model);
        }

        //
        // POST: /InstructionWorks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            InstructionWork model = db.InstructionWorks
                                        .Include(iw => iw.Tasks)
                                        .FirstOrDefault(iw => iw.ID == id);
            model.Tasks.ToList().ForEach(t => db.Tasks.Remove(t));
            model.HistInstructionWorks.ToList().ForEach(t => db.HistInstructionWorks.Remove(t));

            db.SaveChanges();
            db.InstructionWorks.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
