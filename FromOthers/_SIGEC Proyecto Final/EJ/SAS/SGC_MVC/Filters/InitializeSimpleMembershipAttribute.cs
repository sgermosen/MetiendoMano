using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using SGC_MVC.Models;
using System.Web.Security;
using SGC_MVC.CustomCode;

namespace SGC_MVC.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<SASContext>(null);

                try
                {
                    using (var context = new SASContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                    
                    //WebSecurity.InitializeDatabaseConnection("SASContext", "user", "userID", "username", autoCreateTables: true);
                    // Create admin user.
                    if (!WebSecurity.UserExists("mail@eisdr.com"))
                    {
                        WebSecurity.CreateUserAndAccount(
                            "mail@eisdr.com",
                            "123456",
                            propertyValues: new
                            {
                                username = "mail@eisdr.com",
                                password = "123456",
                                name = "EISDR",
                                createDate = DateTime.Now,
                                updateDate = DateTime.Now,
                                //email = "mail@eisdr.com",
                                status = 1,
                                activeKey = "",
                                lastVisitAt = DateTime.Now,
                                superUser = 1,
                            });
                    }
                    var roles = (SimpleRoleProvider)Roles.Provider;
                    if (!roles.RoleExists("Administrators"))
                    {
                        roles.CreateRole("Administrators");
                    }

                    if (!roles.GetRolesForUser("mail@eisdr.com").Contains("Administrators"))
                    {
                        roles.AddUsersToRoles(new[] { "mail@eisdr.com" }, new[] { "Administrators" });
                    }
                    Helper.InsertControllerAndActions();
                    Helper.InsertDefaultData();

                    using(var db = new SASContext())
                    {
                        var adminRole = db.Webpages_Roles.FirstOrDefault(r => r.RoleName == "Administrators");
                        foreach (SGC_MVC.Models.Action a in db.Actions)
                        {
                            if(!adminRole.Actions.Contains(a))
                                adminRole.Actions.Add(a);
                        }
                        db.Entry(adminRole).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
