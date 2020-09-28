using Common;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.DbContextScope.Extensions;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface IInstructorService
    {
        IEnumerable<InstructorCourseForGridView> GetAll(string userId);
        InstructorWidget Widget(string userId);
    }

    public class InstructorService : IInstructorService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Course> _courseRepository;

        public InstructorService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Course> courseRepository
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _courseRepository = courseRepository;
        }

        public IEnumerable<InstructorCourseForGridView> GetAll(string userId)
        {
            var result = new List<InstructorCourseForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var courses = ctx.GetEntity<Course>();
                    var usersPerCourses = ctx.GetEntity<UsersPerCourse>();
                    var incomes = ctx.GetEntity<Income>();

                    var queryIncome = incomes.Where(x =>
                        x.EntityType == Enums.EntityType.Courses
                        && x.IncomeType == Enums.IncomeType.TeacherTotal
                    );

                    var currentYear = DateTime.Now.Year;
                    var currentMonth = DateTime.Now.Month;

                    result = (
                        from c in courses.Where(x => x.AuthorId == userId)
                        select new InstructorCourseForGridView
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            Image = c.Image2,
                            Students = usersPerCourses.Count(x => x.CourseId == c.Id),
                            Total = queryIncome.Where(x =>
                                x.EntityID == c.Id
                            ).Select(x => x.Total).DefaultIfEmpty().Sum(),
                            TotalPerMonth = queryIncome.Where(x =>
                                x.EntityID == c.Id
                                && x.CreatedAt.Value.Year == currentYear
                                && x.CreatedAt.Value.Month == currentMonth
                            ).Select(x => x.Total).DefaultIfEmpty().Sum(),
                        }
                    ).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public InstructorWidget Widget(string userId)
        {
            var result = new InstructorWidget();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var courses = ctx.GetEntity<Course>();
                    var usersPerCourses = ctx.GetEntity<UsersPerCourse>();
                    var incomes = ctx.GetEntity<Income>();
                    var reviewPerCourses = ctx.GetEntity<ReviewsPerCourse>();

                    var queryIncome = incomes.Where(x =>
                        x.EntityType == Enums.EntityType.Courses
                        && x.IncomeType == Enums.IncomeType.TeacherTotal
                    );

                    var queryCourse = courses.Where(x => x.AuthorId == userId);

                    var currentYear = DateTime.Now.Year;
                    var currentMonth = DateTime.Now.Month;

                    result.Total = queryIncome.Where(x =>
                        queryCourse.Any(y => y.Id == x.EntityID)
                    ).Select(x => x.Total).DefaultIfEmpty().Sum();

                    result.TotalPerMonth = queryIncome.Where(x =>
                        queryCourse.Any(y => y.Id == x.EntityID)
                        && x.CreatedAt.Value.Year == currentYear
                        && x.CreatedAt.Value.Month == currentMonth
                    ).Select(x => x.Total).DefaultIfEmpty().Sum();

                    result.Students = usersPerCourses.Where(x =>
                        queryCourse.Any(y => y.Id == x.CourseId)
                    ).Count();

                    result.Reputation = reviewPerCourses.Where(x =>
                        queryCourse.Any(y => y.Id == x.CourseId)
                    ).Select(x => x.Vote).DefaultIfEmpty().Average();
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
