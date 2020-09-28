using LightInject;
using Model.Auth;
using Model.Domain;
using Persistence.DbContextScope;
using Persistence.Repository;

namespace Service.Config
{
    public class ServiceRegister : ICompositionRoot
    {               
        public void Compose(IServiceRegistry container)
        {
            var ambientDbContextLocator = new AmbientDbContextLocator();

            container.Register<IDbContextScopeFactory>((x) => new DbContextScopeFactory(null));
            container.Register<IAmbientDbContextLocator, AmbientDbContextLocator>(new PerScopeLifetime());

            container.Register<IRepository<ApplicationUser>>((x) => new Repository<ApplicationUser>(ambientDbContextLocator));
            container.Register<IRepository<ApplicationRole>>((x) => new Repository<ApplicationRole>(ambientDbContextLocator));
            container.Register<IRepository<ApplicationUserRole>>((x) => new Repository<ApplicationUserRole>(ambientDbContextLocator));
            container.Register<IRepository<Category>>((x) => new Repository<Category>(ambientDbContextLocator));
            container.Register<IRepository<Course>>((x) => new Repository<Course>(ambientDbContextLocator));
            container.Register<IRepository<LessonsPerCourse>>((x) => new Repository<LessonsPerCourse>(ambientDbContextLocator));
            container.Register<IRepository<UsersPerCourse>>((x) => new Repository<UsersPerCourse>(ambientDbContextLocator));
            container.Register<IRepository<Income>>((x) => new Repository<Income>(ambientDbContextLocator));
            container.Register<IRepository<ReviewsPerCourse>>((x) => new Repository<ReviewsPerCourse>(ambientDbContextLocator));
            container.Register<IRepository<CourseLessonLearnedsPerStudent>>((x) => new Repository<CourseLessonLearnedsPerStudent>(ambientDbContextLocator));

            container.Register<IUserService, UserService>();
            container.Register<ICategoryService, CategoryService>();
            container.Register<ICourseService, CourseService>();
            container.Register<IInstructorService, InstructorService>();
            container.Register<ILessonService, LessonService>();
            container.Register<IStudentService, StudentService>();
            container.Register<IWidgetService, WidgetService>();            
        }
    }
}
