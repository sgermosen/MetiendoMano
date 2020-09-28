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
    [CustomAuthorize]
    [IsMenu]
    public class PurposeController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();

        //
        // GET: /Purpose/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            PurposeViewModel model = new PurposeViewModel();
            model.mission = db.Documents
                .FirstOrDefault(d => d.DocumentType.name.ToLower() == "Mision".ToLower() && d.companyID == companyID);
            model.vision = db.Documents
                .FirstOrDefault(d => d.DocumentType.name.ToLower() == "Vision".ToLower() && d.companyID == companyID);
            model.values = db.Documents
                .FirstOrDefault(d => d.DocumentType.name.ToLower() == "Valores".ToLower() && d.companyID == companyID);
            model.policies = db.Documents
                .FirstOrDefault(d => d.DocumentType.name.ToLower() == "Politicas".ToLower() && d.companyID == companyID);
            
            return View(model);
        }

        [IsView]
        public ActionResult Edit(int id = 0)
        {
            var document = db.Documents.Find(id);
            PurposeEditViewModel model = new PurposeEditViewModel();
            model.id = id;
            model.document_text = document.documentText;
            model.docType = document.DocumentType.name;

            return View(model);
        }

        public ActionResult PartialDialog()
        {
            ViewBag.name = "Razon de nueva version";

            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(PurposeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = db.Documents.Find(model.id);
                document.updateDate = DateTime.Now;
                document.documentText = model.document_text;
                db.Entry(document).State = EntityState.Modified;
                if (model.isNewVersion)
                {
                    var hist_document = new HistDocument(document);
                    hist_document.changeReason = model.changeReason;
                    document.version++;
                    db.HistDocuments.Add(hist_document);
                    db.Entry(document).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
