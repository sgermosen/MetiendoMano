using Common;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.DbContextScope.Extensions;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        decimal GetCredits(string userId);
        ResponseHelper Update(ApplicationUser model);
        AnexGRIDResponde GetAll(AnexGRID grid);
    }

    public class UserService : IUserService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<ApplicationUser> _applicationUserRepo;

        public UserService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<ApplicationUser> applicationUserRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _applicationUserRepo = applicationUserRepo;
        }

        public ResponseHelper Update(ApplicationUser model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var originalModel = _applicationUserRepo.Single(x => x.Id == model.Id);

                    originalModel.Name = model.Name;
                    originalModel.LastName = model.LastName;

                    _applicationUserRepo.Update(originalModel);
                    ctx.SaveChanges();

                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }

            return rh;
        }

        public AnexGRIDResponde GetAll(AnexGRID grid)
        {
            grid.Inicializar();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var user = ctx.GetEntity<ApplicationUser>();
                    var roles = ctx.GetEntity<ApplicationRole>();
                    var userRoles = ctx.GetEntity<ApplicationUserRole>();
                    var courses = ctx.GetEntity<Course>();
                    var userCourses = ctx.GetEntity<UsersPerCourse>();

                    var queryRoles = (
                        from r in roles
                        from ur in userRoles.Where(x => x.RoleId == r.Id)
                        select new {
                            UserId = ur.UserId,
                            Role = r.Name
                        }
                    ).AsQueryable();

                    var query = (
                        from u in user
                        select new UserForGridView
                        {
                            Id = u.Id,
                            FullName = u.Name + " " + u.LastName,
                            Email = u.Email,
                            CoursesCreated = courses.Where(x => x.AuthorId == u.Id).Count(),
                            CoursesTaken = userCourses.Where(x => x.UserId == u.Id).Count(),
                            Roles = queryRoles.Where(x => x.UserId == u.Id).Select(x => x.Role).ToList()
                        }
                    ).AsQueryable();

                    // Order by
                    if (grid.columna == "FullName")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.FullName)
                                                             : query.OrderBy(x => x.FullName);
                    }

                    var data = query.Skip(grid.pagina)
                                    .Take(grid.limite)
                                    .ToList();

                    var total = query.Count();

                    grid.SetData(data, total);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return grid.responde();
        }

        public decimal GetCredits(string userId)
        {
            decimal credits = 0;

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    credits = _applicationUserRepo.Find(x => x.Id == userId)
                                                  .Select(x => x.Credit).Single();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return credits;
        }
    }
}
