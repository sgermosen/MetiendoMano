using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using SGC_MVC.CustomCode;
using System.IO;
using Simple.ImageResizer;
using SGC_MVC.Models.ViewModels;


namespace SGC_MVC.Controllers
{
    [CustomAuthorize]
    [IsMenu]
    public class CompanyController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        //
        // GET: /Company/
        [IsView]
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.Rules);
            return View(companies.ToList());
        }

        //
        // GET: /Company/Details/5
        [IsView]
        public ActionResult Details(int id = 0)
        {
            Company company = db.Companies.Include(c => c.Rules).FirstOrDefault(c => c.ID == id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // GET: /Company/Create
        [CustomAuthorize]
        [IsView]
        public ActionResult Create()
        {
            ViewBag.rulesCompany = db.Rules.ToSelectListItems(r => r.code + " - " + r.name, r => r.ID.ToString());
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Create(CompanyViewModel company, int[] rulesCompany, HttpPostedFileBase logoImage)
        {
            if (ModelState.IsValid)
            {
                company.status = false;

                var imageGUID = Guid.NewGuid();
                if (logoImage != null)
                {

                    string imageUrl = imageGUID +
                        Path.GetExtension(logoImage.FileName);
                    string filePath = Path.Combine(
                        Server.MapPath("~/Uploads/CompanyLogos"),
                        imageGUID + Path.GetExtension(logoImage.FileName
                    ));
                    if (logoImage.ContentLength > 102400)
                    {
                        MemoryStream target = new MemoryStream();
                        logoImage.InputStream.CopyTo(target);
                        var resizedImage = new ImageResizer(target.ToArray());
                        resizedImage.Resize(400, 200, false, ImageEncoding.Jpg90);
                        resizedImage.SaveToFile(filePath);
                    }
                    else
                    {
                        logoImage.SaveAs(filePath);
                    }
                }

                Company realCompany = new Company();
                realCompany.name = company.name;
                realCompany.companyText = company.companyText;
                realCompany.description = company.description;
                realCompany.email = company.email;
                if (logoImage != null)
                    realCompany.logo = imageGUID + Path.GetExtension(logoImage.FileName);
                else
                    realCompany.logo = "";

                realCompany.status = company.status;
                if (rulesCompany != null)
                    foreach (int r in rulesCompany)
                    {
                        var rule = db.Rules.Find(r);
                        realCompany.Rules.Add(rule);
                        rule.Companies.Add(realCompany);
                    }

                db.Companies.Add(realCompany);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rulesCompany = db.Rules.ToSelectListItems(r => r.code + " - " + r.name, r => r.ID.ToString());

            return View(company);
        }

        //
        // GET: /Company/Edit/5
        [CustomAuthorize]
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Company company = db.Companies.Include(c => c.Rules).FirstOrDefault(c => c.ID == id);
            CompanyViewModel irealCompany = new CompanyViewModel();

            irealCompany.name = company.name;
            irealCompany.companyText = company.companyText;
            irealCompany.description = company.description;
            irealCompany.email = company.email;
            irealCompany.logo = company.logo;
            irealCompany.status = company.status;
            int[] selectedRules = company.Rules.Select(c => c.ID).ToArray();
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.rulesCompany = db.Rules.ToSelectListItems(r => r.code + " - " + r.name, r => r.ID.ToString(), rl => selectedRules.Contains(rl.ID));
            return View(irealCompany);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Edit(CompanyViewModel company, HttpPostedFileBase logoImage)
        {
            if (ModelState.IsValid)
            {
                Company comp = db.Companies.Include(c => c.Rules).FirstOrDefault(c => c.ID == company.ID);
                comp.Rules.Clear();

                db.Entry(comp).State = EntityState.Modified;

                var imageGUID = Guid.NewGuid();
                if (logoImage != null)
                {
                    string imageUrl = imageGUID +
                        Path.GetExtension(logoImage.FileName);
                    string filePath = Path.Combine(
                        Server.MapPath("~/Uploads/CompanyLogos"),
                        imageGUID + Path.GetExtension(logoImage.FileName
                    ));

                    if (logoImage.ContentLength > 102400)
                    {
                        MemoryStream target = new MemoryStream();
                        logoImage.InputStream.CopyTo(target);
                        var resizedImage = new ImageResizer(target.ToArray());
                        resizedImage.Resize(400, 200, false, ImageEncoding.Jpg90);
                        resizedImage.SaveToFile(filePath);
                    }
                    else
                    {
                        logoImage.SaveAs(filePath);
                    }

                    var images = Directory.GetFiles(Server.MapPath("~/Uploads/CompanyLogos"));
                    FileInfo old = new FileInfo(Server.MapPath("~/Uploads/CompanyLogos")+"\\"+company.logo);
                    if (images.Contains(old.FullName))
                    {
                        filePath = logoImage.FileName;
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/CompanyLogos"), company.logo));
                        company.logo = filePath;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(company.filename))
                    {
                        var images = Directory.GetFiles(Server.MapPath("~/Uploads/CompanyLogos"));
                        FileInfo old = new FileInfo(Server.MapPath("~/Uploads/CompanyLogos") + "\\" + company.logo);
                        if (images.Contains(old.FullName))
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/CompanyLogos"), company.logo));
                            company.logo = "";
                        }
                    }
                }

                comp.name = company.name;
                comp.companyText = company.companyText;
                comp.description = company.description;
                comp.email = company.email;

                if (logoImage != null)
                    comp.logo = imageGUID + Path.GetExtension(logoImage.FileName);
                else
                    comp.logo = "";

                comp.status = company.status;
                if (company.rulesCompany != null)
                    foreach (int r in company.rulesCompany)
                    {
                        var rule = db.Rules.Find(r);
                        comp.Rules.Add(rule);
                    }
                db.Entry(comp).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rulesCompany = db.Rules.ToSelectListItems(r => r.code + " - " + r.name, r => r.ID.ToString(), rl => company.rulesCompany.Contains(rl.ID));
            return View(company);
        }

        //
        // GET: /Company/Delete/5
        [CustomAuthorize]
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5
        [CustomAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            company.Rules.Clear();
            db.Companies.Remove(company);
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