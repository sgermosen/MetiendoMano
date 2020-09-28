using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using System.Data.Entity;
using SGC_MVC.CustomCode;
using SGC_MVC.Models.ViewModels;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class FormsController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();
        //
        // GET: /Forms/
        [IsView]
        public ActionResult Index()
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var forms = db.Forms.Include(frm => frm.FormFields).Where(frm => frm.companyID == companyID);

            return View(forms.ToList());
        }

        //
        // GET: /Forms/Details/5
        [IsView]
        public ActionResult Details(int id)
        {
            var form = db.Forms.Include(frm => frm.FormFields).FirstOrDefault(frm => frm.ID == id);

            return View(form);
        }

        //
        // GET: /Forms/Create
        [IsView]
        public ActionResult Create()
        {
            int? companyID = Helper.GetCompanyID(db);
            ViewBag.ruleID = Helper.GetRulesSelect(db);
            ViewBag.processID = new SelectList(db.Processes.Where(pc => pc.companyID == companyID), "ID", "name");

            return View();
        }

        //
        // POST: /Forms/Create

        [HttpPost]
        public ActionResult Create(Form model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    model.createUser = WebSecurity.CurrentUserId;
                    model.companyID = (int)Helper.GetCompanyID(db);
                    model.version = 1;
                    model.statusID = (int)Helper.StatusTypes.Creacion;
                    db.Forms.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Edit", new { id = model.ID });
                }
                // TODO: Add insert logic here
                int? companyID = Helper.GetCompanyID(db);
                ViewBag.ruleID = Helper.GetRulesSelect(db);
                ViewBag.processID = new SelectList(db.Processes.Where(pc => pc.companyID == companyID), "ID", "name");
                return View(model);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }

        }

        //
        // GET: /Forms/Edit/5
        [IsView]
        public ActionResult Edit(int id)
        {
            int? companyID = Helper.GetCompanyID(db);
            var form = db.Forms.AsNoTracking().Include(frm => frm.FormFields).FirstOrDefault(frm => frm.ID == id);
            ViewBag.processID = new SelectList(db.Processes.Where(pc => pc.companyID == companyID), "ID", "name", form.processID);
            ViewBag.ruleID = Helper.GetRulesSelect(db, new int[]{ form.ruleID});
            var model = new FormViewModel(form);

            return View(model);
        }

        //
        // POST: /Forms/Edit/5

        [HttpPost]
        public ActionResult Edit(FormViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var frm = db.Forms.Find(model.ID);
                    model.UpdateData(frm);
                    frm.updateDate = DateTime.Now.ToUniversalTime();
                    db.SaveChanges();
                    if (model.submitVal == "Agregar")
                    {
                        return RedirectToAction("AddField", new { formId = model.ID });
                    }
                    else if (model.submitVal == "nueva version")
                    {
                        HistForm hFrm = new HistForm(frm);
                        hFrm.changeReason = model.changeReason;
                        frm.version++;
                        db.HistForms.Add(hFrm);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (model.submitVal == "Eliminar Campo")
                    {
                        return RedirectToAction("DeleteField", new { id = model.fieldId });
                    }
                    else if (model.submitVal == "Actualizar Campo")
                    {
                        return RedirectToAction("EditField", new { id = model.fieldId });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                int? companyID = Helper.GetCompanyID(db);
                ViewBag.processID = new SelectList(db.Processes.Where(pc => pc.companyID == companyID), "ID", "name", model.processID);
                ViewBag.ruleID = Helper.GetRulesSelect(db, new int[] { model.ruleID });
                model.FormFields = db.FormFields.Where(ff => ff.formID == model.ID).ToList();

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Forms/Delete/5
        [IsView]
        public ActionResult Delete(int id)
        {
            var form = db.Forms.Find(id);

            if (form == null)
            {
                return HttpNotFound();
            }
            ViewBag.rule = db.Rules.Find(form.ruleID);

            return View(form);
        }

        //
        // POST: /Forms/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var form = db.Forms.Find(id);
                foreach (FormField ff in form.FormFields.ToList())
                {
                    db.FormFields.Remove(ff);
                }
                db.Forms.Remove(form);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [IsView]
        public ActionResult AddField(int formId)
        {
            var model = new FormField();
            model.formID = formId;
            ViewBag.fieldTypeID = new SelectList(db.FieldTypes, "ID", "GetName");

            return View(model);
        }

        [HttpPost]
        public ActionResult AddField(FormField model)
        {
            if (ModelState.IsValid)
            {
                db.FormFields.Add(model);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.formID });
            }
            var fieldTypeID = new SelectList(db.FieldTypes, "ID", "GetName", model.fieldTypeID);

            return View(model);
        }

        [IsView]
        public ActionResult EditField(int id)
        {
            var model = db.FormFields.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.fieldTypeID = new SelectList(db.FieldTypes, "ID", "GetName", model.fieldTypeID);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditField(FormField model, string[] frmFieldOpts, string otherProprs = null)
        {
            if (ModelState.IsValid)
            {
                model.FormFieldOptions.Clear();
                foreach (FormFieldOption opt in db.FormFieldOptions.Where(ffo => ffo.formFieldID == model.ID).ToList())
                {
                    db.FormFieldOptions.Remove(opt);
                }
                if (frmFieldOpts != null)
                {
                    foreach (string r in frmFieldOpts)
                    {
                        string[] row = r.Split('|');
                        FormFieldOption option = new FormFieldOption();
                        option.label = row[0];
                        option.value = row[1];
                        option.defaultValue = Convert.ToBoolean(row[2]);
                        option.formFieldID = model.ID;
                        db.FormFieldOptions.Add(option);
                    }
                }

                if (!string.IsNullOrEmpty(otherProprs))
                {
                    var vals = otherProprs.Split('|');
                    FormFieldOption opt = new FormFieldOption();
                    opt.label = vals[0];
                    opt.value = vals[1];
                    opt.formFieldID = model.ID;
                    opt.isOtherOption = true;
                    db.FormFieldOptions.Add(opt);
                }

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = model.formID });
            }
            ViewBag.fieldTypeID = new SelectList(db.FieldTypes, "ID", "GetName", model.fieldTypeID);

            return View(model);
        }

        [IsView]
        public ActionResult DeleteField(int id)
        {
            var model = db.FormFields.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.fieldTypeID = new SelectList(db.FieldTypes, "ID", "GetName", model.fieldTypeID);

            return View(model);
        }

        [HttpPost, ActionName("DeleteField")]
        public ActionResult DeleteFieldConfirmed(int id)
        {
            var formField = db.FormFields.Find(id);
            db.FormFields.Remove(formField);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = formField.formID });
        }

        public ActionResult GetFieldOptions(int fieldTypeId, int formFieldID)
        {
            var formField = db.FormFields.Find(formFieldID);
            switch ((Helper.FieldTypes)fieldTypeId)
            {
                case Helper.FieldTypes.Numerico:
                {
                    return PartialView("PartialNumericField", formField);
                }
                case Helper.FieldTypes.EMail:
                {
                    return PartialView("PartialEmailField", formField);
                }
                case Helper.FieldTypes.TextoSimple:
                {
                    return PartialView("PartialSimpleTextField", formField);
                }
                case Helper.FieldTypes.Comentarios:
                {
                    return PartialView("PartialCommentField", formField);
                }
                case Helper.FieldTypes.Fecha:
                {
                    return PartialView("PartialDateField", formField);
                }
                case Helper.FieldTypes.ListaDesplegable:
                case Helper.FieldTypes.ListaRadioButton:
                {
                    return PartialView("PartialDropDownField", formField);
                }
                case Helper.FieldTypes.ListaMultiSelect:
                {
                    return PartialView("PartialMultiSelectField", formField);
                }
                default:
                {
                    return PartialView("Default", formField);
                }
            }
        }
    }
}
