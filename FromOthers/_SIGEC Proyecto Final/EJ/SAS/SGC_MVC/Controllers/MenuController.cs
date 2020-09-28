using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGC_MVC.Models;
using WebMatrix.WebData;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Controllers
{
    //[IsMenu]
    public class MenuController : System.Web.Mvc.Controller
    {
        private SASContext db = new SASContext();

        [IsView]
        public ActionResult admin()
        {

            var menus = db.SubMenus.GroupBy(d => d.name).Select(grp => grp.FirstOrDefault());
            ViewBag.Menu = db.Menus.ToList();
            return View(menus.ToList());
        }

        [HttpPost]
        public ActionResult admin(FormCollection form)
        {
            List<SubMenu> submenus = db.SubMenus.ToList();

            foreach(SubMenu sb in db.SubMenus){
                db.SubMenus.Remove(sb);
            }
            db.SaveChanges();

            List<int> order = new List<int>();

            foreach (string menu in form.AllKeys)
            {
                string[] composition = menu.Split('-');
                int menuID = int.Parse(composition[0]);
                int subMenuID = int.Parse(composition[1]);

                SubMenu submenu = new SubMenu();
                SubMenu oldSubmenu =  submenus.FirstOrDefault(d=>d.ID==subMenuID);
                //submenu = oldSubmenu;
                
                submenu.menuID = menuID;
                submenu.Menu = db.Menus.FirstOrDefault(d => d.ID == menuID);
                submenu.noOrder = order.Where(d => d == menuID).Count()+1;
                submenu.name = oldSubmenu.name;
                submenu.viewID = oldSubmenu.viewID;
                submenu.View = oldSubmenu.View;


                order.Add(menuID);
                db.SubMenus.Add(submenu);
            }
            db.SaveChanges();



            var menus = db.SubMenus.GroupBy(d => d.name).Select(grp => grp.FirstOrDefault());
            ViewBag.Menu = db.Menus.ToList();
            return View(menus.ToList());
        }

        //
        // GET: /Menu/
        [IsView]
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.User);
            return View(menus.ToList());
        }

        //
        // GET: /Menu/Details/5
        public ActionResult Details(int id = 0)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // GET: /Menu/Create
        [IsView]
        public ActionResult Create()
        {
            Menu model = new Menu();
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            model.noOrder = db.Menus.Where(dep => dep.companyID == companyID).OrderByDescending(d => d.noOrder).FirstOrDefault().noOrder + 1;
            ViewBag.createUser = new SelectList(db.Users, "ID", "name");
            return View(model);
        }

        //
        // GET: /Menu/Create
        [IsView]
        public ActionResult CreateChild(int id)
        {
            SubMenu subMenu = new SubMenu();
            subMenu.menuID = id;
            subMenu.noOrder = db.SubMenus.Where(d => d.menuID == id).Count() + 1;
            subMenu.Menu = db.Menus.FirstOrDefault(d => d.ID == id);
            ViewBag.viewID = new SelectList(db.Views, "ID", "name");
            return View(subMenu);
        }

        [HttpPost]
        public ActionResult CreateChild(SubMenu menu)
        {
            if (ModelState.IsValid)
            {
                db.SubMenus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = menu.menuID });
            }
            return View(menu);
        }

        //
        // POST: /Menu/Create

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.createUser = WebSecurity.CurrentUserId;
                menu.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.createUser = new SelectList(db.Users, "ID", "name", menu.createUser);
            return View(menu);
        }

        //
        // GET: /Menu/Edit/5
        [IsView]
        public ActionResult Edit(int id = 0)
        {
            Menu menu = db.Menus.Find(id);
            menu.companyID = (int)db.Users.Find(WebSecurity.CurrentUserId).companyID;
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.createUser = new SelectList(db.Users, "ID", "name", menu.createUser);
            return View(menu);
        }

        [IsView]
        public ActionResult EditChild(int id = 0)
        {
            SubMenu menu = db.SubMenus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }

            /*ViewBag.viewID = db.Views
                .ToSelectListItems(
                t => t.name, t => t.viewID.ToString(),
                t => t.viewID == menu.viewID);*/

            ViewBag.viewID = new SelectList(db.Views, "ID", "name",menu.viewID);
            return View(menu);
        }

        [HttpPost]
        public ActionResult EditChild(SubMenu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = menu.menuID });
            }
            return View(menu);
        }

        //
        // POST: /Menu/Edit/5

        [HttpPost]
        public ActionResult Edit(Menu menu, string submitVal)
        {
            if (ModelState.IsValid)
            {
                int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
                menu.companyID = (int)companyID;
                menu.createUser = WebSecurity.CurrentUserId;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.createUser = new SelectList(db.Users, "ID", "name", menu.createUser);

            if (submitVal == "Agregar")
            {
                return RedirectToAction("CreateChild", new { id = menu.ID });
            }
            else
                return View(menu);
            
        }

        //
        // GET: /Menu/Delete/5
        [IsView]
        public ActionResult Delete(int id = 0)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // POST: /Menu/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            //menu.SubMenus.Clear();
            foreach(SubMenu subMenu in menu.SubMenus.ToList()){
                db.SubMenus.Remove(subMenu);
            }
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Menu/Delete/5
        [IsView]
        public ActionResult DeleteChild(int id = 0)
        {
            SubMenu menu = db.SubMenus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // POST: /Menu/Delete/5

        [HttpPost, ActionName("DeleteChild")]
        public ActionResult DeleteChildConfirmed(int id)
        {
            SubMenu menu = db.SubMenus.Find(id);
            db.SubMenus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = menu.menuID });
        }

        /// <summary>
        /// Actualiza el orden de los capitulos usando el EDT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromPosition"></param>
        /// <param name="toPosition"></param>
        /// <param name="direction"></param>
        public void UpdateOrder(int id, int fromPosition, int toPosition, string direction)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;

            if (direction == "back")
            {
                var movedMenu = db.Menus.Where(dep => dep.companyID == companyID).Where(d => (toPosition <= d.noOrder && d.noOrder <= fromPosition)).ToList();

                foreach (var menu in movedMenu)
                {
                    menu.noOrder++;
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                var movedMenu = db.Menus.Where(dep => dep.companyID == companyID)
                            .Where(d => (fromPosition <= d.noOrder && d.noOrder <= toPosition))
                            .ToList();
                foreach (var company in movedMenu)
                {
                    company.noOrder--;
                }
            }

            var men = db.Menus.FirstOrDefault(d => d.ID == id);
            men.noOrder = toPosition;
            db.Entry(men).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Actualiza el orden de los capitulos usando el EDT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromPosition"></param>
        /// <param name="toPosition"></param>
        /// <param name="direction"></param>
        public void UpdateSubOrder(int id, int fromPosition, int toPosition, string direction)
        {

            var suby = db.SubMenus.FirstOrDefault(d => d.ID == id);
            int menuid = suby.menuID;
            if (direction == "back")
            {
                var movedSubMenu = db.SubMenus.Where(d => (d.menuID == menuid && toPosition <= d.noOrder && d.noOrder <= fromPosition)).ToList();

                foreach (var sub in movedSubMenu)
                {
                    sub.noOrder++;
                    db.Entry(sub).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                var movedSubMenu = db.Menus
                            .Where(d => (d.ID == menuid && fromPosition <= d.noOrder && d.noOrder <= toPosition))
                            .ToList();
                foreach (var sub in movedSubMenu)
                {
                    sub.noOrder--;
                }
            }

            suby.noOrder = toPosition;
            db.Entry(suby).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
