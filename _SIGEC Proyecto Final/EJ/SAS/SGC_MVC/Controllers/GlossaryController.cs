using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using WebMatrix.WebData;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    [IsMenu]
    public class GlossaryController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Glossary/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var glossaries = db.Glossaries.Include(g => g.Rules).Where(dep => dep.companyID == companyID);
            return View(glossaries.ToList());
        }

        //
        // GET: /Glossary/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Glossary glossary = db.Glossaries.Find(id);
            if (glossary == null)
            {
                return HttpNotFound();
            }
            return View(glossary);
        }

        //
        // GET: /Glossary/Create
        [IsView]
        public ActionResult Create()
        {
            GlossaryViewModel model = new GlossaryViewModel();

            model.allRules = Helper.GetRulesSelect(db);

            return View(model);
        }

        //
        // POST: /Glossary/Create

        [HttpPost]
        public ActionResult Create(GlossaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var glossary = db.Glossaries.FirstOrDefault(
                    g => (g.term == model.term) && (g.definition == model.definition)
                    );

                if (glossary == null)
                {
                    Glossary g = new Glossary();
                    g.term = model.term;
                    g.definition = model.definition;
                    g.createUser = WebSecurity.CurrentUserId;
                    g.companyID = (int)db.Users.Find(g.createUser).companyID;
                    db.Glossaries.Add(g);

                    if(model.selectedRules!=null)
                    foreach (int ruleId in model.selectedRules)
                    {
                        SGC_MVC.Models.Rule r = db.Rules.Find(ruleId);
                        g.Rules.Add(r);
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            model.allRules = Helper.GetRulesSelect(db);

            return View(model);
        }

        //
        // GET: /Glossary/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            var glossary = db.Glossaries.Include(g => g.Rules)
                .FirstOrDefault(g => g.ID == id);
            if (glossary == null)
            {
                return HttpNotFound();
            }

            GlossaryViewModel model = new GlossaryViewModel();
            var selectedRules = from r in glossary.Rules
                                select r.ID;

            model.selectedRules = selectedRules.ToArray();
            model.allRules = Helper.GetRulesSelect(db);
            model.term = glossary.term;
            model.definition = glossary.definition;
            model.glossaryID = glossary.ID;

            return View(model);
        }

        //
        // POST: /Glossary/Edit/5

        [HttpPost]
        public ActionResult Edit(GlossaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Glossary g = db.Glossaries.Include(gs => gs.Rules)
                    .FirstOrDefault(gs => gs.ID == model.glossaryID);
                if (g.Rules != null)
                {
                    g.Rules.Clear();
                    db.SaveChanges();
                }

                if(model.selectedRules != null)
                foreach (int ruleId in model.selectedRules)
                {
                    SGC_MVC.Models.Rule r = db.Rules.Find(ruleId);
                    g.Rules.Add(r);
                    db.SaveChanges();
                }
                g.term = model.term;
                g.definition = model.definition;
                g.updateDate = DateTime.Now;
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Glossary/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Glossary glossary = db.Glossaries.Find(id);
            if (glossary == null)
            {
                return HttpNotFound();
            }
            return View(glossary);
        }

        //
        // POST: /Glossary/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Glossary glossary = db.Glossaries.Find(id);
            glossary.Rules.Clear();
            db.Glossaries.Remove(glossary);
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