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
    public class ProceduresController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Procedures/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var procedures = db.Procedures.Where(dep => dep.companyID == companyID).Include(p => p.User)
                                  .Include(p => p.Glossaries)
                                  .Include(p => p.Status).Include(p => p.Process)
                                  .Include(p => p.User);

            return View(procedures.ToList());
        }

        //
        // GET: /Procedures/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyLogo = Helper.GetCompanyImage(db);
            ViewBag.histProcedures = db.HistProcedures.Where(hp => hp.procedureID == id).ToList();

            return View(procedure);
        }

        //
        // GET: /Procedures/Create
        [IsView]
        public ActionResult Create()
        {

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.Find(companyID);
            ViewBag.ruleID = c.Rules.ToSelectListItems
                (
                    t => t.code + " - " + t.name,
                    t => t.ID.ToString()
                );

            ViewBag.processID = new SelectList(db.Processes.Where(pr => pr.companyID == companyID), "ID", "name");
            ViewBag.responsible = new SelectList(db.Positions.Where(pos => pos.companyID == companyID), "ID", "name");
            Procedure model = new Procedure();

            return View(model);
        }

        //
        // POST: /Procedures/Create

        [HttpPost]
        public ActionResult Create(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                procedure.createUser = WebSecurity.CurrentUserId;
                procedure.companyID = (int)db.Users.Find(procedure.createUser).companyID;
                procedure.statusID = (int)Helper.StatusTypes.Creacion;
                db.Procedures.Add(procedure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ruleID = db.Rules.ToSelectListItems
                (
                 t => t.code + " - " + t.name,
                 t => t.ID.ToString()
                );

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.processID = new SelectList(db.Processes.Where(dep => dep.companyID == companyID), "ID", "name");
            ViewBag.responsible = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name");

            return View(procedure);
        }

        public ActionResult UpdateGlossaries(int procedureID, int[] selected = null)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.FirstOrDefault(cp => cp.ID == companyID);
            var procedure = db.Procedures.Include(p => p.User)
                                  .FirstOrDefault(p => p.ID == procedureID);
            var glossaryID = db.Glossaries.Where(gl => gl.companyID == c.ID).ToSelectListItems
                (
                 g => g.term,
                 g => g.ID.ToString(),
                 g => selected.Contains(g.ID)
                );

            return Json(glossaryID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateNormatives(int procedureID, int[] selected = null)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.FirstOrDefault(cp => cp.ID == companyID);
            var procedure = db.Procedures.Include(p => p.User)
                                  .FirstOrDefault(p => p.ID == procedureID);
            var normativas = db.Documents.Where
                (
                 doc => doc.documentTypeID == (int)Helper.DocumentTypes.BaseLegal
                     && doc.companyID == c.ID
                ).ToList();
            var normativeID = normativas.ToSelectListItems
                (
                 norm => norm.title,
                 norm => norm.ID.ToString(),
                 norm => (selected == null) ? false : selected.Contains(norm.ID)
                );

            return Json(normativeID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateReferences(int procedureID, int[] selected = null)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.FirstOrDefault(cp => cp.ID == companyID);
            var procedure = db.Procedures.Include(p => p.User)
                                  .FirstOrDefault(p => p.ID == procedureID);

            var documents = db.Documents.Where(doc => doc.companyID == c.ID);
            var docReferences = documents.ToSelectListItems(
                 dr => dr.DocumentType.name + ".  " + dr.title,
                 dr => dr.ID.ToString(),
                 dr => selected.Contains(dr.ID));

            return Json(docReferences, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDoc(int documentType)
        {
            string view = "", action = "Create";
            switch ((Helper.DocumentTypes)documentType)
            {
                case Helper.DocumentTypes.BaseLegal: { view = "BaseLegal"; } break;
                case Helper.DocumentTypes.Politicas: { view = "Policies"; } break;
                case Helper.DocumentTypes.Manual: { view = "Manual"; } break;
                case Helper.DocumentTypes.Objetivo: { view = "Objectives"; } break;
                default: { view = "Document"; } break;
            }

            return Json(Url.Action(action, view), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Procedures/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            var procedure = db.Procedures.Include(p => p.User)
                                  .Include(p => p.Glossaries)
                                  .Include(p => p.Status).Include(p => p.Process)
                                  .Include(p => p.User).Include(p => p.ProcedureActivities)
                                  .Include(p => p.Documents).Include(p => p.Documents1)
                                  .Include(p => p.Documents2)
                                  .FirstOrDefault(p => p.ID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.FirstOrDefault(cp => cp.ID == companyID);

            ViewBag.processID = new SelectList(db.Processes.Where(ps => ps.companyID == c.ID), "ID", "name", procedure.processID);
            ViewBag.responsible = new SelectList(db.Positions.Where(pos => pos.companyID == c.ID), "ID", "name", procedure.responsible);
            ViewBag.ruleID = c.Rules.ToSelectListItems
                (
                 t => t.code + " - " + t.name,
                 t => t.ID.ToString(),
                 t => t.ID == procedure.ruleID
                );
            ViewBag.responsibleAct = new SelectList(
                    db.Users.Where(usr => usr.positionID != null && usr.companyID == c.ID),
                    "ID",
                    "name"
                );
            ViewBag.glossaryID = db.Glossaries.Where(gl => gl.companyID == c.ID).ToSelectListItems
                (
                 g => g.term,
                 g => g.ID.ToString(),
                 g => procedure.Glossaries.Select(gs => gs.ID).ToArray().Contains(g.ID)
                );

            var normativas = db.Documents.Where
                (
                 doc => doc.documentTypeID == (int)Helper.DocumentTypes.BaseLegal
                     && doc.companyID == c.ID
                ).ToList();
            ViewBag.normativeID = normativas.ToSelectListItems
                (
                 norm => norm.DocumentType.name + ".  " + norm.title,
                 norm => norm.ID.ToString(),
                 norm => procedure.Documents1.Select(dc => dc.ID).ToArray().Contains(norm.ID)
                );
            var documents = db.Documents.Where(doc => doc.companyID == c.ID);
            ViewBag.docReferences = documents.ToSelectListItems(
                 dr => dr.DocumentType.name + ".  " + dr.title,
                 dr => dr.ID.ToString(),
                 dr => procedure.Documents2.Select(pd => pd.ID).ToList().Contains(dr.ID));
            ViewBag.docAnnexes = documents.ToSelectListItems(
                 dr => dr.DocumentType.name + ".  " + dr.title,
                 dr => dr.ID.ToString(),
                 dr => procedure.Documents.Select(pd => pd.ID).ToList().Contains(dr.ID));
            var DocTypes = db.DocumentTypes.Where(dt => dt.ID != (int)Helper.DocumentTypes.Norma
                                                     && dt.ID != (int)Helper.DocumentTypes.Mision
                                                     && dt.ID != (int)Helper.DocumentTypes.Vision
                                                     && dt.ID != (int)Helper.DocumentTypes.Valores
                                                     && dt.ID != (int)Helper.DocumentTypes.Politica);
            ViewBag.documentTypeID = DocTypes.ToList().ToSelectListItems(
                dt => dt.name,
                dt => dt.ID.ToString());

            return View(procedure);
        }

        //
        // POST: /Procedures/Edit/5

        [HttpPost]
        public ActionResult Edit
            (
                Procedure procedure, int[] docReferences, int[] docAnnexes,
                int[] glossaryID, int[] normativeID, string submitVal
            )
        {
            if (ModelState.IsValid)
            {
                procedure.updateDate = DateTime.Now;
                db.Entry(procedure).State = EntityState.Modified;
                db.SaveChanges();


                var proc = db.Procedures.Include(pd => pd.Glossaries)
                    .Include(pd => pd.Documents).Include(pd => pd.Documents1)
                    .Include(pd => pd.Documents2).FirstOrDefault(pd => pd.ID == procedure.ID);
                proc.Glossaries.Clear();
                proc.Annexes.Clear();
                proc.Normatives.Clear();
                proc.References.Clear();
                if (docReferences != null)
                {
                    foreach (int dr in docReferences)
                    {
                        var dR = db.Documents.Find(dr);
                        proc.References.Add(dR);
                    }
                }

                if (docAnnexes != null)
                {
                    foreach (int da in docAnnexes)
                    {
                        var dA = db.Documents.Find(da);
                        proc.Annexes.Add(dA);
                    }
                }

                if (normativeID != null)
                {
                    foreach (int dn in normativeID)
                    {
                        var dN = db.Documents.Find(dn);
                        proc.Normatives.Add(dN);
                    }
                }

                if (glossaryID != null)
                {
                    foreach (int pg in glossaryID)
                    {
                        var gl = db.Glossaries.Find(pg);
                        proc.Glossaries.Add(gl);
                    }
                }

                if (submitVal == "nueva version")
                {
                    HistProcedure hp = new HistProcedure(proc);
                    hp.changeReason = procedure.changeReason;
                    proc.version++;
                    db.HistProcedures.Add(hp);
                }

                db.Entry(proc).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }



            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var c = db.Companies.FirstOrDefault(cp => cp.ID == companyID);

            ViewBag.processID = new SelectList(db.Processes.Where(dep => dep.companyID == companyID), "ID", "name", procedure.processID);
            ViewBag.responsible = new SelectList(db.Users.Where(dep => dep.companyID == companyID).Where(usr => usr.positionID != null), "ID", "name", procedure.responsible);
            ViewBag.ruleID = c.Rules.ToSelectListItems
                (
                 t => t.code + " - " + t.name,
                 t => t.ID.ToString(),
                 t => t.ID == procedure.ruleID
                );
            ViewBag.responsibleAct = new SelectList(
                    db.Users.Where(usr => usr.companyID == companyID && usr.positionID != null),
                    "ID",
                    "name"
                );
            ViewBag.glossaryID = db.Glossaries.Where(gl => gl.companyID == c.ID).ToSelectListItems
                (
                 g => g.term,
                 g => g.ID.ToString(),
                 g => procedure.Glossaries.Where(dep => dep.companyID == companyID).Select(gs => gs.ID).ToArray().Contains(g.ID)
                );

            var normativas = db.Documents.Where(dep => dep.companyID == companyID).Where
                (
                 doc => doc.documentTypeID == (int)Helper.DocumentTypes.BaseLegal
                ).ToList();
            ViewBag.normativeID = normativas.ToSelectListItems
                (
                 norm => norm.title,
                 norm => norm.ID.ToString(),
                 norm => procedure.Documents.Select(dc => dc.ID).ToArray().Contains(norm.ID)
                );

            var documents = db.Documents.Include(doc => doc.Procedures).ToList();
            ViewBag.docReferences = documents.ToSelectListItems(
                 dr => dr.DocumentType.name + ".  " + dr.title,
                 dr => dr.ID.ToString(),
                 dr => docReferences.Contains(dr.ID));
            ViewBag.docAnnexes = documents.ToSelectListItems(
                 dr => dr.DocumentType.name + ".  " + dr.title,
                 dr => dr.ID.ToString(),
                 dr => docAnnexes.Contains(dr.ID));
            var DocTypes = db.DocumentTypes.Where(dt => dt.ID != (int)Helper.DocumentTypes.Norma
                                                     && dt.ID != (int)Helper.DocumentTypes.Mision
                                                     && dt.ID != (int)Helper.DocumentTypes.Vision
                                                     && dt.ID != (int)Helper.DocumentTypes.Valores
                                                     && dt.ID != (int)Helper.DocumentTypes.Politica);
            ViewBag.documentTypeID = DocTypes.ToList().ToSelectListItems(
                dt => dt.name,
                dt => dt.ID.ToString());

            return View(procedure);
        }

        //
        // GET: /Procedures/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            var procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        //
        // POST: /Procedures/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Procedure procedure = db.Procedures.Find(id);
            foreach (ProcedureActivity act in procedure.ProcedureActivities.ToList())
            {
                db.ProcedureActivities.Remove(act);
            }
            procedure.ProcedureActivities.Clear();
            procedure.Annexes.Clear();
            procedure.References.Clear();
            procedure.Normatives.Clear();
            db.Procedures.Remove(procedure);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult GetActivities(int procedureId)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            var procedureActivities = db.ProcedureActivities
                                          .Where(pa => pa.procedureID == procedureId).ToList();


            return Json(
                new
                {
                    aaData = procedureActivities.Select(
                      p => new[] {
                           p.User1.name,
                           p.activity,
                           p.ID.ToString()
                       }
                    )
                },
                JsonRequestBehavior.AllowGet
             );
        }

        public string AddActivity(int responsible, string activity, int procedureId)
        {
            if (string.IsNullOrEmpty(activity) || responsible == 0 || procedureId == 0)
            {
                return "Error en los datos";
            }
            ProcedureActivity act = new ProcedureActivity();
            act.activity = activity;
            act.procedureID = procedureId;
            act.responsible = responsible;
            act.createDate = DateTime.Now;
            act.createUser = WebSecurity.CurrentUserId;
            db.ProcedureActivities.Add(act);
            db.SaveChanges();

            return "Actividad Guardada Correctamente";
        }

        [HttpPost]
        public string DeleteActivity(int id)
        {
            var activity = db.ProcedureActivities.Find(id);
            if (activity == null)
            {
                return "Error al eliminar la actividad, verifique sus datos";
            }
            db.ProcedureActivities.Remove(activity);
            db.SaveChanges();

            return "Actividad eliminada correctamente";
        }

        public ActionResult EditActivity(int id)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var activity = db.ProcedureActivities
                                .Include(pa => pa.User1)
                                .FirstOrDefault(pa => pa.ID == id);
            ViewBag.responsibleModal = new SelectList(
                    db.Users.Where(usr => usr.positionID != null && usr.companyID == companyID),
                    "ID",
                    "name",
                    activity.User1.ID
                );

            return PartialView(activity);
        }

        [HttpPost]
        public string EditActivity(string activity, int responsible, int activityId)
        {
            var act = db.ProcedureActivities.Find(activityId);

            if (act == null)
            {
                return "Datos incorrectos, por favor revise sus datos";
            }
            act.activity = activity;
            act.updateDate = DateTime.Now;
            act.responsible = responsible;
            db.Entry(act).State = EntityState.Modified;
            db.SaveChanges();

            return "Datos de actividad actualizados correctamente";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}