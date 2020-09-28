using SGC_MVC.CustomCode;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    [CustomAuthorize]
    public class BaseLegalController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();
        //
        // GET: /BaseLegal/

        [IsView]
        public ActionResult Index()
        {
            int baseLegalId = (int)Helper.DocumentTypes.BaseLegal;
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var baseLegalDocs = db.Documents.Where(bld => bld.documentTypeID == baseLegalId && bld.companyID == companyID);
            return View(baseLegalDocs.ToList());
        }

        [IsView]
        public ActionResult Create()
        {
            var model = new LegalBasisViewModel();
            model.documentTypeID = (int)Helper.DocumentTypes.BaseLegal;
            return View(model);
        }

        [IsView]
        public ActionResult Edit(int id)
        {
            var model = db.Documents.Find(id);

            LegalBasisViewModel m = new LegalBasisViewModel();
            m.name = model.title;
            m.description = model.description;
            m.url = model.url;
            m.docId = model.ID;
            m.createUser = model.createUser;
            m.companyID = model.companyID;
            m.documentTypeID = model.documentTypeID;
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(LegalBasisViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = "";
                bool replaced = false;
                Document d = db.Documents.Find(model.docId);
                if (model.Document != null)
                {
                    if (model.Document.FileName != null)
                    {
                        var documents = Directory.GetFiles(Server.MapPath("~/Uploads"));
                        if (documents.Contains(model.url))
                        {
                            FileInfo old = new FileInfo(model.url);
                            if (model.Document.ContentLength != (int)old.Length)
                            {
                                filePath = model.Document.FileName;
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), model.url));
                                replaced = true;
                                model.Document.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), filePath));
                                d.url = replaced ? filePath : model.url;
                            }
                        }
                        else
                        {
                            int newID = d.ID;
                            filePath = newID + "$$" + Path.GetFileName(model.Document.FileName);
                            model.Document.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), filePath));
                            d.url = filePath;
                        }
                    }
                }

                if (model.Document == null || model.Document.FileName == null)
                {
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), model.url));
                    d.url = "";
                }

                d.title = model.name;
                d.companyID = (int)db.Users.Find(d.createUser).companyID;
                d.description = model.description;
                d.url = (d.url == null) ? "" : d.url;

                db.Entry(d).State = System.Data.EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);
        }

        [HttpPost]
        [IsView]
        public ActionResult Create(LegalBasisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var docGUID = Guid.NewGuid();
                string filePath = "";
                Document d = new Document();
                if (model.Document != null)
                {
                    if (model.Document.FileName != null)
                    {
                        docGUID = Guid.NewGuid();
                        var last = db.Documents.OrderByDescending(id => id.ID).FirstOrDefault();
                        int newID = 0;
                        if (last != null)
                        {
                            newID = last.ID + 1;
                        }
                        filePath = newID + "$$" + Path.GetFileName(model.Document.FileName);
                        model.Document.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), filePath));
                    }
                }
                d.createUser = WebSecurity.CurrentUserId;
                d.title = model.name;
                d.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
                d.description = model.description;
                d.documentText = "";
                d.EDT = 0;
                d.documentTypeID = db.DocumentTypes.FirstOrDefault(dt => dt.name == "Base Legal").ID;
                d.url = filePath;
                d.statusID = 1;

                db.Documents.Add(d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [IsView]
        public ActionResult Delete(int id = 0)
        {
            var document = db.Documents.Find(id);

            return View(document);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {

            Document document = db.Documents.Find(ID);
            if (!string.IsNullOrEmpty(document.url))
            {
                System.IO.File.Delete(document.url);
            }

            db.Documents.Remove(document);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
