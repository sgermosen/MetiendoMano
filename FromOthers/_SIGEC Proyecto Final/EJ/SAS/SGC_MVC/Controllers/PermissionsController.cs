using SGC_MVC.CustomCode;
using SGC_MVC.Models;
using SGC_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class PermissionsController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();

        //
        // GET: /Permissions/
        [IsView]
        public ActionResult Edit()
        {
            ViewBag.RoleId = new SelectList(db.Webpages_Roles.ToList(), "RoleID", "RoleName");
            ViewBag.controllers = db.Controllers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PermissionsEditModel model)
        {
            if (ModelState.IsValid)
            {
                Webpages_Roles role = db.Webpages_Roles.Find(model.RoleId);
                role.Actions.Clear();

                if(model.actions != null)
                    foreach (int action in model.actions)
                    {
                        role.Actions.Add(db.Actions.Find(action));
                    }

                role.Departments.Clear();
                if(model.departments != null)
                    foreach (int department in model.departments)
                    {
                        role.Departments.Add(db.Departments.Find(department));
                    }

                role.Status.Clear();
                if(model.statuses != null)
                    foreach (int status in model.statuses) 
                    {
                        role.Status.Add(db.Status.Find(status));
                    }

                db.Entry(role).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.RoleId = new SelectList(db.Webpages_Roles.Include("actions").ToList(), "RoleID", "RoleName", model.RoleId);
            ViewBag.controllers = db.Controllers.ToList();
            return View(model);
        }

        //deprecated
        public ActionResult GetActions(int controller_id)
        {
            var controller = db.Controllers.FirstOrDefault(c => c.ID == controller_id);
            List<SelectListItem> actions = new List<SelectListItem>();

            foreach (var data in controller.Actions)
            {
                actions.Add(new SelectListItem() {
                    Text = data.name,
                    Value = data.ID.ToString() 
                });
            }
            
            return Json(actions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMenus(int role_id)
        {
            var role = db.Webpages_Roles.FirstOrDefault(r => r.RoleID == role_id);
            var controllers = db.Controllers.ToList();
            PermissionsEditPartial model = new PermissionsEditPartial();

            List<int> actions = new List<int>();
            List<int> departments = new List<int>();
            List<int> statuses = new List<int>();

            role.Actions.ToList()
                .ForEach(a => actions.Add(a.ID));
            role.Departments.ToList()
                .ForEach(d => departments.Add(d.ID));
            role.Status.ToList()
                .ForEach(s => statuses.Add(s.ID));

            model.SelectedActions = actions.ToArray();
            model.SelectedStatuses = statuses.ToArray();
            model.SelectedDepartments = departments.ToArray();
            model.controllers = controllers;
            model.departments = db.Departments.ToList();
            model.statuses = db.Status.ToList();

            return PartialView(model);
        }

    }
}
