using Common;
using Common.ProjectHelpers;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.DbContextScope.Extensions;
using Persistence.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Service
{
    public interface IWidgetService
    {
        WidgetIncome Incomes(bool month);
        WidgetStatistics Variance();
        WidgetStatistics Average();
    }

    public class WidgetService : IWidgetService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Income> _incomeRepo;
        private readonly IRepository<UsersPerCourse> _userPerCourseRepo;
        private readonly IRepository<Course> _courseRepo;

        public WidgetService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Income> incomeRepo,
            IRepository<UsersPerCourse> userPerCourseRepo,
            IRepository<Course> courseRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _incomeRepo = incomeRepo;
            _userPerCourseRepo = userPerCourseRepo;
            _courseRepo = courseRepo;
        }

        public WidgetIncome Incomes(bool month)
        {
            var result = new WidgetIncome();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var queryIncome = _incomeRepo.AsQueryable();

                    if (month)
                    {
                        queryIncome = queryIncome.Where(x =>
                            x.CreatedAt.Value.Month == DateTime.Now.Month
                            && x.CreatedAt.Value.Year == DateTime.Now.Year
                        );
                    }

                    result.Total = queryIncome.Where(x =>
                        x.IncomeType == Enums.IncomeType.Total
                    ).Sum(x => x.Total);

                    result.Company = queryIncome.Where(x =>
                        x.IncomeType == Enums.IncomeType.CompanyTotal
                    ).Sum(x => x.Total);

                    result.Instructors = queryIncome.Where(x =>
                        x.IncomeType == Enums.IncomeType.TeacherTotal
                    ).Sum(x => x.Total);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public WidgetStatistics Variance()
        {
            var result = new WidgetStatistics();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var dateEnd = DateTime.Today;
                    var dateStart = DateTime.Today.AddMonths(-1);

                    #region Get Incomes
                    var incomes = _incomeRepo.Find(x => (
                        (x.CreatedAt.Value.Year == dateStart.Year || x.CreatedAt.Value.Year == dateEnd.Year)
                        && (x.CreatedAt.Value.Month == dateStart.Month || x.CreatedAt.Value.Month == dateEnd.Month)
                        && x.IncomeType == Enums.IncomeType.CompanyTotal
                    )).GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Sum(y => y.Total)
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .ToList();

                    // Si no hay nada, no hubo variación
                    if (incomes == null)
                    {
                        result.IncomeCompany = 0;
                    }
                    else
                    {
                        var priorMonth = incomes.Where(x =>
                            x.Year == dateStart.Year && x.Month == dateStart.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        var currentMonth = incomes.Where(x =>
                            x.Year == dateEnd.Year && x.Month == dateEnd.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        if (priorMonth == 0)
                        {
                            result.IncomeCompany = 0;
                        }
                        else
                        {
                            result.IncomeCompany = Math.Round(((currentMonth - priorMonth) / priorMonth) * 100, 2);
                        }
                    }
                    #endregion

                    #region Get Students
                    var students = _userPerCourseRepo.Find(x => (
                        (x.CreatedAt.Value.Year == dateStart.Year || x.CreatedAt.Value.Year == dateEnd.Year)
                        && (x.CreatedAt.Value.Month == dateStart.Month || x.CreatedAt.Value.Month == dateEnd.Month)
                    )).GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Count()
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .ToList();

                    // Si no hay nada, no hubo variación
                    if (students == null)
                    {
                        result.NewStudents = 0;
                    }
                    else
                    {
                        var priorMonth = students.Where(x =>
                            x.Year == dateStart.Year && x.Month == dateStart.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        var currentMonth = students.Where(x =>
                            x.Year == dateEnd.Year && x.Month == dateEnd.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        if (priorMonth == 0)
                        {
                            result.NewStudents = 0;
                        }
                        else
                        {
                            result.NewStudents = ((Convert.ToDecimal(currentMonth) - priorMonth) / priorMonth) * 100;
                        }
                    }
                    #endregion

                    #region Get Courses
                    var courses = _courseRepo.Find(x => (
                        (x.CreatedAt.Value.Year == dateStart.Year || x.CreatedAt.Value.Year == dateEnd.Year)
                        && (x.CreatedAt.Value.Month == dateStart.Month || x.CreatedAt.Value.Month == dateEnd.Month)
                    )).GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Count()
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .ToList();

                    // Si no hay nada, no hubo variación
                    if (courses == null)
                    {
                        result.NewCourses = 0;
                    }
                    else
                    {
                        var priorMonth = courses.Where(x =>
                            x.Year == dateStart.Year && x.Month == dateStart.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        var currentMonth = courses.Where(x =>
                            x.Year == dateEnd.Year && x.Month == dateEnd.Month
                        ).Select(x => x.Total).DefaultIfEmpty(0).SingleOrDefault();

                        if (priorMonth == 0)
                        {
                            result.NewCourses = 0;
                        }
                        else
                        {
                            result.NewCourses = ((Convert.ToDecimal(currentMonth) - priorMonth) / priorMonth) * 100;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public WidgetStatistics Average()
        {
            var result = new WidgetStatistics();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    #region Get Incomes
                    result.IncomeCompany = _incomeRepo.Find(x => 
                        x.IncomeType == Enums.IncomeType.CompanyTotal
                    ).GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Sum(y => y.Total)
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .Select(x => x.Total).DefaultIfEmpty(0).Average();
                    #endregion

                    #region Get Students
                    result.NewStudents = (decimal) _userPerCourseRepo.AsQueryable().GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new
                    {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Count()
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .Select(x => x.Total).DefaultIfEmpty(0).Average();
                    #endregion

                    #region Get Courses
                    result.NewCourses = (decimal) _courseRepo.AsQueryable().GroupBy(x => new {
                        Year = x.CreatedAt.Value.Year,
                        Month = x.CreatedAt.Value.Month
                    }).Select(x => new {
                        Year = x.Key.Year,
                        Month = x.Key.Month,
                        Total = x.Count()
                    }).OrderByDescending(x => x.Year)
                      .ThenByDescending(x => x.Month)
                      .Select(x => x.Total).DefaultIfEmpty(0).Average();
                    #endregion
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
