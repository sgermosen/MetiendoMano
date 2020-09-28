using Common;
using Common.ProjectHelpers;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using Newtonsoft.Json;
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
    public interface ICategoryService
    {
        ResponseHelper InsertOrUpdate(Category model);
        AnexGRIDResponde GetAll(AnexGRID grid);
        IEnumerable<Category> GetAll();
        Category Get(int id);
        ResponseHelper Delete(int id);
        string GetForMenu();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Category> _categoryRepo;

        public CategoryService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Category> categoryRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _categoryRepo = categoryRepo;
        }

        public ResponseHelper InsertOrUpdate(Category model)
        {
            var rh = new ResponseHelper();
            var newRecord = false;

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.Id > 0)
                    {
                        var originalCategory = _categoryRepo.Single(x => x.Id == model.Id);

                        originalCategory.Name = model.Name;
                        originalCategory.Icon = model.Icon;
                        originalCategory.Slug = Slug.Category(model.Id, model.Name);

                        _categoryRepo.Update(originalCategory);
                    }
                    else
                    {
                        newRecord = true;
                        // Después de insertar el campo Id ya tiene ID
                        _categoryRepo.Insert(model);
                    }

                    ctx.SaveChanges();
                }

                if (newRecord)
                {
                    using (var ctx = _dbContextScopeFactory.Create())
                    {
                        // Obtenemos el registro insertado
                        var originalCategory = _categoryRepo.Single(x => x.Id == model.Id);

                        originalCategory.Slug = Slug.Category(model.Id, model.Name);

                        // Actualizamos
                        _categoryRepo.Update(originalCategory);

                        ctx.SaveChanges();
                    }
                }

                Parameters.CategoryList = GetForMenu();

                rh.SetResponse(true);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }

            return rh;
        }

        public IEnumerable<Category> GetAll()
        {
            var result = new List<Category>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _categoryRepo.GetAll().OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public AnexGRIDResponde GetAll(AnexGRID grid)
        {
            grid.Inicializar();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var courses = ctx.GetEntity<Course>();
                    var categories = ctx.GetEntity<Category>();
                    var students = ctx.GetEntity<UsersPerCourse>();

                    var queryStudents = (
                        from c in courses
                        from s in students.Where(x => x.CourseId == c.Id)
                        select new
                        {
                            UserId = s.UserId,
                            CategoryId = c.CategoryId
                        }
                    ).AsQueryable();

                    var query = (
                        from c in categories
                        select new CategoryForGridView
                        {
                            Id = c.Id,
                            Icon = c.Icon,
                            Name = c.Name,
                            Courses = courses.Where(x => x.CategoryId == c.Id).Count(),
                            Students = queryStudents.Where(x => x.CategoryId == c.Id).Count()
                        }
                    ).AsQueryable();

                    // Order by
                    if (grid.columna == "Name")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Name)
                                                             : query.OrderBy(x => x.Name);
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

        public string GetForMenu()
        {
            var result = "[]";

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = JsonConvert.SerializeObject(
                        _categoryRepo.GetAll()
                        .OrderBy(x => x.Name)
                        .Select(x => new
                        {
                            Icon = x.Icon,
                            Name = x.Name,
                            Slug = x.Slug
                        }).ToList()
                    );
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public Category Get(int id)
        {
            var result = new Category();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _categoryRepo.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper Delete(int id)
        {
            var result = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var hasCourses = _categoryRepo.Find(x =>
                        x.Id == id
                        && x.Courses.Any()
                    ).Any();

                    if (!hasCourses)
                    {
                        var originalCategoria = _categoryRepo.Single(x => x.Id == id);
                        _categoryRepo.Delete(originalCategoria);

                        result.SetResponse(true);
                    }
                    else
                    {
                        result.SetResponse(false, "Esta categoría no puede ser eliminado debido a que tiene cursos asignados.");
                    }

                    ctx.SaveChanges();
                }

                if (result.Response)
                {
                    Parameters.CategoryList = GetForMenu();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
    }
}
