using Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Model.Auth;
using NLog;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });

            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public async Task<IdentityResult> CreateWithDefaultRole(ApplicationUser model, string password)
        {
            try
            {
                await CreateAsync(model, password);

                using (var ctx = new ApplicationDbContext())
                {
                    // Obtener el usuario
                    var userId = ctx.ApplicationUser.Single(x => x.Email == model.Email).Id;

                    // Obtener el Role
                    var roleId = ctx.ApplicationRole.Single(x => x.Name == RoleNames.User).Id;

                    // Registramos la relación del role con el user
                    ctx.Entry(new ApplicationUserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    }).State = EntityState.Added;

                    ctx.SaveChanges();
                }

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return await Task.FromResult(new IdentityResult(ex.Message));
            }
        }

        public async override Task<IdentityResult> AddToRoleAsync(string userId, string roleId)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.ApplicationUserRole.Add(new ApplicationUserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    });

                    ctx.SaveChanges();
                }

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return await Task.FromResult(new IdentityResult(ex.Message));
            }
        }

        public async override Task<IdentityResult> AddToRolesAsync(string userId, params string[] roles)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    foreach (var roleId in roles)
                    {
                        ctx.ApplicationUserRole.Add(new ApplicationUserRole
                        {
                            UserId = userId,
                            RoleId = roleId
                        });
                    }

                    ctx.SaveChanges();
                }

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return await Task.FromResult(new IdentityResult(ex.Message));
            }
        }

        public async new Task<IEnumerable<ApplicationRole>> GetRolesAsync(string userId)
        {
            IEnumerable<ApplicationRole> result = new List<ApplicationRole>();

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var roles = ctx.ApplicationUserRole.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
                    result = ctx.ApplicationRole.Where(x => roles.Contains(x.Id)).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return await Task.FromResult(result);
        }

        public async override Task<bool> IsInRoleAsync(string userId, string roleId)
        {
            var result = false;

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    result = ctx.ApplicationUserRole.Any(x => x.UserId == userId && x.RoleId == roleId);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return await Task.FromResult(result);
        }

        public async override Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleId)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var roles = ctx.ApplicationUserRole.Where(x => x.RoleId == roleId && x.UserId == userId).ToList();
                    ctx.Entry(roles).State = EntityState.Deleted;

                    ctx.SaveChanges();
                }

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return await Task.FromResult(new IdentityResult(ex.Message));
            }
        }

        public async override Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var rolesPerUser = ctx.ApplicationUserRole.Where(x => roles.Contains(x.RoleId) && x.UserId == userId).ToList();
                    ctx.Entry(rolesPerUser).State = EntityState.Deleted;

                    ctx.SaveChanges();
                }

                return await Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return await Task.FromResult(new IdentityResult(ex.Message));
            }
        }
    }
}