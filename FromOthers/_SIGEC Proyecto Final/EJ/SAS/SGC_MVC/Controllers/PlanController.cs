using SGC_MVC.CustomCode;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class PlanController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Plan/

        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var plans = db.Plans.Where(d => d.companyID == companyID);
            ViewBag.fResponsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "name", "name");
            
            return View(plans.ToList());
        }

        //
        // GET: /Plan/Details/5

        [IsView]
        public ActionResult Details(int id = 0)
        {
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            ViewBag.histPlans = db.HistPlans.Where(hp => hp.planID == id).ToList();
            ViewBag.companyLogo = Helper.GetCompanyImage(db);
            return View(plan);
        }

        //
        // GET: /Plan/Create
        [IsView]
        public ActionResult Create()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.companyID = new SelectList(db.Companies, "ID", "name");
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name");
            ViewBag.processID = new SelectList(db.Processes.Where(p => p.companyID == companyID), "ID", "name");
            return View();
        }

        //
        // POST: /Plan/Create

        [HttpPost]
        public ActionResult Create(Plan plan)
        {
            if (ModelState.IsValid)
            {
                int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
                plan.createUser = WebSecurity.CurrentUserId;
                plan.companyID = companyID.Value;
                db.Plans.Add(plan);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = plan.ID });
            }

            ViewBag.companyID = new SelectList(db.Companies, "ID", "name", plan.companyID);
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == plan.companyID), "ID", "name", plan.responsible);
            ViewBag.processID = new SelectList(db.Processes.Where(p => p.companyID == plan.companyID), "ID", "name");
            return View(plan);
        }

        //
        // GET: /Plan/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Plan model = db.Plans.Find(id);
            
            if (model == null)
            {
                return HttpNotFound();
            }
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name", model.responsible);
            ViewBag.processID = new SelectList(db.Processes.Where(p => p.companyID == companyID), "ID", "name", model.processID);
            return View(model);
        }

        //
        // POST: /Plan/Edit/5

        [HttpPost]
        public ActionResult Edit(Plan plan, string submitVal)
        {
            if (ModelState.IsValid)
            {
                plan.updateDate = DateTime.Now;

                if (submitVal == "nueva version")
                {
                    HistPlan hp = new HistPlan(plan);
                    hp.changeReason = plan.changeReason;
                    plan.version++;
                    db.HistPlans.Add(hp);
                }

                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
                if (submitVal == "Agregar")
                    return RedirectToAction("AddObjective", new { planID = plan.ID });
                return RedirectToAction("Index");
            }
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name", plan.responsible);
            ViewBag.processID = new SelectList(db.Processes.Where(p => p.companyID == companyID), "ID", "name", plan.processID);
            return View(plan);
        }

        //
        // GET: /Plan/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        //
        // POST: /Plan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Plan plan = db.Plans.Find(id);
            foreach (PlanObjective po in plan.PlanObjectives.ToList())
            {
                foreach (ObjectiveResource or in po.ObjectiveResources.ToList())
                {
                    db.ObjectiveResources.Remove(or);
                }
                db.PlanObjectives.Remove(po);
            }
            db.Plans.Remove(plan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetResources(int objectiveID)
        {
            var objectiveResources = db.ObjectiveResources
                                          .Where(or => or.objectiveID == objectiveID).ToList();


            return Json(
                new
                {
                    aaData = objectiveResources.Select(
                      p => new[] {
                           p.infrastructure,
                           p.humans,
                           p.ID.ToString()
                       }
                    )
                },
                JsonRequestBehavior.AllowGet
             );
        }

        public ActionResult AddObjective(int planID)
        {
            ViewBag.planID = planID;
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name");

            return View();
        }

        [HttpPost]
        public ActionResult AddObjective(PlanObjective model)
        {
            if (ModelState.IsValid)
            {
                model.createUser = WebSecurity.CurrentUserId;
                db.PlanObjectives.Add(model);
                db.SaveChanges();

                return View("EditObjective", new { id = model.ID });
            }
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.planID = model.planID;
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name");

            return View(model);
        }

        public ActionResult EditObjective(int id = 0)
        {
            PlanObjective model = db.PlanObjectives.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            int? companyID = Helper.GetCompanyID(db); 
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name", model.responsible);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditObjective(PlanObjective model) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.planID });
            }
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.responsible = new SelectList(db.Positions.Where(d => d.companyID == companyID), "ID", "name", model.responsible);

            return View(model);
        }

        public ActionResult DeleteObjective(int id = 0)
        {
            var model = db.PlanObjectives.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("DeleteObjective")]
        public ActionResult DeleteObjectiveConfirmed(int id)
        {
            var model = db.PlanObjectives.Find(id);
            foreach (ObjectiveResource or in model.ObjectiveResources.ToList())
            {
                db.ObjectiveResources.Remove(or);
            }
            db.PlanObjectives.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = model.planID });
        }

        public string AddResource(string infrastructure, string humans, int objectiveID)
        {
            if (string.IsNullOrEmpty(infrastructure) || string.IsNullOrEmpty(humans) || objectiveID <= 0)
                return "Error en los datos";
            ObjectiveResource pr = new ObjectiveResource();
            pr.infrastructure = infrastructure;
            pr.humans = humans;
            pr.objectiveID = objectiveID;

            db.ObjectiveResources.Add(pr);
            db.SaveChanges();

            return "Recurso guardado correctamente";
        }

        public ActionResult EditResource(int id = 0)
        {
            var resource = db.ObjectiveResources.Find(id);

            return PartialView(resource);
        }

        [HttpPost]
        public string EditResource(string infrastructure, string humans, int resourceID)
        {
            var resource = db.ObjectiveResources.Find(resourceID);

            if (resource == null)
                return "Error al procesar los datos, recurso incorrecto";

            resource.infrastructure = infrastructure;
            resource.humans = humans;

            db.Entry(resource).State = EntityState.Modified;
            db.SaveChanges();

            return "Datos del recurso actualizados correctamente";
        }

        [HttpPost]
        public string DeleteResource(int id)
        {
            var resource = db.ObjectiveResources.Find(id);

            if (resource == null)
            {
                return "Recurso no encontrado, favor actualizar ventana";
            }

            db.ObjectiveResources.Remove(resource);
            db.SaveChanges();
            return "Recurso eliminado satisfactoriamente";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}