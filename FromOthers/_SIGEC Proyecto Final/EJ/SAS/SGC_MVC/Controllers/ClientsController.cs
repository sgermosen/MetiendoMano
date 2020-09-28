using SGC_MVC.CustomCode;
using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using System.Web.Script.Serialization;
using System.Data.Common;

namespace SGC_MVC.Controllers
{
    [IsMenu]
    public class ClientsController : System.Web.Mvc.Controller
    {
        //context global object
        private SASContext db = new SASContext();

        //
        // GET: /Clients/
        [IsView]
        public ActionResult Index()
        {

            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            var clients = db.Entities.Where(c => c.entityTypeID == (int)Helper.EntityTypes.Cliente && c.companyID == companyID);

            return View(clients);
        }

        //
        // GET: /Clients/Details/5
        [IsView]
        public ActionResult Details(int id)
        {
            Entity client = db.Entities.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        //
        // GET: /Clients/Create
        [IsView]
        public ActionResult Create()
        {
            Entity model = new Entity();
            return View(model);
        }

        //
        // POST: /Clients/Create

        [HttpPost]
        public ActionResult Create(Entity model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.entityTypeID = (int)Helper.EntityTypes.Cliente;
                    model.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
                    model.createUser = WebSecurity.CurrentUserId;
                    model.status = false;
                    db.Entities.Add(model);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                var exc = new HandleErrorInfo(ex, "Clients", "Create");
                return View("Error", exc);
            }
        }

        //
        // GET: /Clients/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            var model = db.Entities.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        //
        // POST: /Clients/Edit/5

        [HttpPost]
        public ActionResult Edit(Entity model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.updateDate = DateTime.Now;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                var exc = new HandleErrorInfo(ex, "Clients", "Edit");
                return View("Error", exc);
            }
        }

        //
        // GET: /Clients/Delete/5
        [IsView]
        public ActionResult Delete(int id)
        {
            Entity model = db.Entities.Find(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
        //
        // POST: /Clients/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Entity client = db.Entities.Find(id);
                db.Entities.Remove(client);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error", null);
            }
        }
    }
}
