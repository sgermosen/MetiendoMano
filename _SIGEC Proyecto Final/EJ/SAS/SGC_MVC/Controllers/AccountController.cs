using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SGC_MVC.Filters;
using SGC_MVC.Models;
using SGC_MVC.CustomCode;
using Simple.ImageResizer;
using System.IO;
using System.Text;

namespace SGC_MVC.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [IsMenu]
    public class AccountController : System.Web.Mvc.Controller
    {
        SASContext db = new SASContext();

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                User user = db.Users.FirstOrDefault(usr => usr.username == model.UserName);
                user.lastVisitAt = DateTime.Now;
                db.Entry(user).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "El nombre de usuario o la contraseña es incorrecto.");
            return View(model);
        }

        public JsonResult CheckUsername(string username)
        {
            MembershipUser usrName = Membership.GetUser(username);

            if (usrName != null)
            {
                return Json(
                    ErrorCodeToString(
                    MembershipCreateStatus.DuplicateEmail
                    ),
                    JsonRequestBehavior.AllowGet
                );
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/EditAccount
        [IsView]
        public ActionResult EditAccount(int id = 0)
        {
            var user = db.Users.Include(u => u.Webpages_Roles).FirstOrDefault(u => u.ID == id);

            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new EditAccountModel(user);
            model.roles = user.Webpages_Roles.Select(wr => wr.RoleName).ToArray();
            ViewBag.AllRoles = db.Webpages_Roles.ToList();
            User currentUser = db.Users.Find(WebSecurity.CurrentUserId);
            int? companyID = currentUser.companyID;
            ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name", model.department_id);
            ViewBag.position_id = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", model.position_id);
            ViewBag.roles = new MultiSelectList(db.Webpages_Roles, "RoleName", "RoleName", model.roles);
            ViewBag.adminUser = (currentUser.superUser) ? true : false;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAccount(EditAccountModel model, bool adminUser)
        {

            if (ModelState.IsValid)
            {
                var imageGUID = Guid.NewGuid();
                string imageUrl = "";
                if (model.image != null)
                {
                    imageUrl = imageGUID +
                        Path.GetExtension(model.image.FileName);
                    string filePath = Path.Combine(
                        Server.MapPath("~/Uploads/UserImages"),
                        imageGUID + Path.GetExtension(model.image.FileName
                    ));
                    if (model.image.ContentLength > 102400)
                    {
                        MemoryStream target = new MemoryStream();
                        model.image.InputStream.CopyTo(target);
                        var resizedImage = new ImageResizer(target.ToArray());
                        resizedImage.Resize(400, 200, false, ImageEncoding.Jpg90);
                        resizedImage.SaveToFile(filePath);
                    }
                    else
                    {
                        model.image.SaveAs(filePath);
                    }

                    var images = Directory.GetFiles(Server.MapPath("~/Uploads/UserImages"));
                    FileInfo old = new FileInfo(Server.MapPath("~/Uploads/UserImages") + "\\" + model.imageUrl);
                    if (images.Contains(old.FullName))
                    {
                        filePath = model.image.FileName;
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/UserImages"), model.imageUrl));
                        model.imageUrl = filePath;
                    }
                    model.imageUrl = imageUrl;
                }
                else if (model.image == null && model.imageUrl != null)
                {
                    var images = Directory.GetFiles(Server.MapPath("~/Uploads/UserImages"));
                    FileInfo old = new FileInfo(Server.MapPath("~/Uploads/UserImages") + "\\" + model.imageUrl);
                    if (images.Contains(old.FullName))
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads/UserImages"), model.imageUrl));
                        model.imageUrl = "";
                    }
                }

                if (adminUser && model.userID == WebSecurity.CurrentUserId)
                {
                    model.position_id = null;
                    model.department_id = null;
                }
                User usr = db.Users.Find(model.userID);
                usr.name = model.name;
                usr.password = model.Password;
                usr.username = model.UserName;
                usr.imageUrl = model.imageUrl;
                if (adminUser)
                {
                    usr.departmentID = model.department_id;
                    usr.positionID = model.position_id;
                    usr.status = Convert.ToInt32(model.activeUser);
                    usr.Webpages_Roles.Clear();
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    var roleProvider = (SimpleRoleProvider)Roles.Provider;
                    if (model.roles != null)
                        roleProvider.AddUsersToRoles(new[] { model.UserName }, model.roles);
                    return RedirectToAction("Index", "Home", null);
                }

            }
            User currentUser = db.Users.Find(WebSecurity.CurrentUserId);
            int? companyID = currentUser.companyID;
            ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name", model.department_id);
            ViewBag.position_id = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", model.position_id);
            ViewBag.roles = new MultiSelectList(db.Webpages_Roles, "RoleName", "RoleName", model.roles);
            ViewBag.adminUser = (currentUser.superUser) ? true : false;

            return View(model);
        }


        //GET: /Account/Register

        [CustomAuthorize]
        [IsView]
        public ActionResult Register()
        {
            ViewBag.roles = new MultiSelectList(db.Webpages_Roles.ToList(), "RoleName", "RoleName");
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name");
            ViewBag.position_id = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name");
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Register(RegisterModel model)
        {
            int? companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    string imageUrl = "";
                    var imageGUID = Guid.NewGuid();
                    if (model.image != null)
                    {

                        imageUrl = imageGUID +
                            Path.GetExtension(model.image.FileName);
                        string filePath = Path.Combine(
                            Server.MapPath("~/Uploads/UserImages"),
                            imageGUID + Path.GetExtension(model.image.FileName
                            ));
                        if (model.image.ContentLength > 102400)
                        {
                            MemoryStream target = new MemoryStream();
                            model.image.InputStream.CopyTo(target);
                            var resizedImage = new ImageResizer(target.ToArray());
                            resizedImage.Resize(400, 200, false, ImageEncoding.Jpg90);
                            resizedImage.SaveToFile(filePath);
                        }
                        else
                        {
                            model.image.SaveAs(filePath);
                        }
                    }
                    WebSecurity.CreateUserAndAccount(
                        model.UserName,
                        model.Password,
                        new
                        {
                            username = model.UserName,
                            password = model.Password,
                            name = model.name,
                            //email = model.email,
                            status = model.status,
                            superUser = model.superUser,
                            activeKey = "",
                            companyID = db.Users.Find(WebSecurity.CurrentUserId).companyID,
                            departmentID = model.department_id,
                            createUser = WebSecurity.CurrentUserId,
                            positionID = model.position_id,
                            imageUrl = (model.image != null) ? imageUrl : null
                        });
                    var roleProvider = (SimpleRoleProvider)Roles.Provider;
                    roleProvider.AddUsersToRoles(new[] { model.UserName }, model.roles);
                    //ViewBag.roles = new MultiSelectList(db.Webpages_Roles.ToList(), "RoleName", "RoleName");
                    //ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name", model.department_id);
                    //ViewBag.position_id = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", model.position_id);
                    //ViewBag.Done = "Usuario registrado satisfactoriamente";
                    return RedirectToAction("Index","Users");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            ViewBag.roles = new MultiSelectList(db.Webpages_Roles.ToList(), "RoleName", "RoleName");
            ViewBag.department_id = new SelectList(db.Departments.Where(dep => dep.companyID == companyID), "ID", "name", model.department_id);
            ViewBag.position_id = new SelectList(db.Positions.Where(dep => dep.companyID == companyID), "ID", "name", model.position_id);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [CustomAuthorize]
        [IsView]
        public ActionResult Details()
        {
            var users = db.Users.ToList();

            return View(users);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        [IsView]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        [IsView]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña ha sido cambiada."
                : message == ManageMessageId.SetPasswordSuccess ? "Su contraseña ha sido establecida."
                : message == ManageMessageId.RemoveLoginSuccess ? "El inicio de sesion externa ha sido removido."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "La contraseña actual es incorrecta o la nueva contraseña es invalida.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Este nombre de usuario ya existe. Seleccione un nombre de usuario diferente.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
