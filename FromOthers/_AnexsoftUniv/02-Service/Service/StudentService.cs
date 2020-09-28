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
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace Service
{
    public interface IStudentService
    {
        IEnumerable<CoursePerStudentListView> GetAll(string userId);
        ResponseHelper Review(ReviewsPerCourse model);
        LessonLandingPage Get(int courseId, int lessonId, string userId);
        ResponseHelper MarkAsLearned(int courseId, int lessonId, string userId);
    }

    public class StudentService : IStudentService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Course> _courseRepo;
        private readonly IRepository<UsersPerCourse> _userPerCourseRepo;
        private readonly IRepository<ReviewsPerCourse> _reviewPerCourseRepo;
        private readonly IRepository<LessonsPerCourse> _lessonPerCourseRepo;
        private readonly IRepository<CourseLessonLearnedsPerStudent> _lessonLearnedRepo;

        public StudentService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Course> courseRepo,
            IRepository<UsersPerCourse> userPerCourseRepo,
            IRepository<ReviewsPerCourse> reviewPerCourseRepo,
            IRepository<LessonsPerCourse> lessonPerCourseRepo,
            IRepository<CourseLessonLearnedsPerStudent> lessonLearnedRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _courseRepo = courseRepo;
            _userPerCourseRepo = userPerCourseRepo;
            _reviewPerCourseRepo = reviewPerCourseRepo;
            _lessonPerCourseRepo = lessonPerCourseRepo;
            _lessonLearnedRepo = lessonLearnedRepo;
        }

        public LessonLandingPage Get(int courseId, int lessonId, string userId)
        {
            var result = new LessonLandingPage();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    // Cuando no se especifica la lección, debemos encontrar la primera para el curso
                    if (lessonId == 0)
                    {
                        lessonId = _lessonPerCourseRepo.Find(x => x.CourseId == courseId)
                                            .OrderBy(x => x.Order)
                                            .First().Id;
                    }

                    // Obtenemos la lección actual y el curso (mediante su relación [Includes])
                    var lesson = _lessonPerCourseRepo.Single(x => 
                        x.Id == lessonId,
                        x => x.Course
                    );

                    // Query base para saber si el alumno ha marcado como aprendida la lección o no
                    var learnedQuery = _lessonLearnedRepo.Find(x =>
                        x.UserId == userId
                        && !x.Deleted
                    ).AsQueryable();

                    result = new LessonLandingPage
                    {
                        Id = lesson.Id,
                        Content = lesson.Content,
                        CourseId = lesson.CourseId,
                        CourseName = lesson.Course.Name,
                        Name = lesson.Name,
                        Learned = learnedQuery.Where(y => y.LessonId == lesson.Id && y.UserId == userId).Any(),
                        Lessons = _lessonPerCourseRepo.Find(x => x.CourseId == lesson.CourseId).OrderBy(x => x.Order).ToList().Select(x => new LessonListLandingPage {
                            CourseId = x.CourseId,
                            Id = x.Id,
                            Name = x.Name,
                            Learned = learnedQuery.Where(y => y.LessonId == x.Id && y.UserId == userId).Any()
                        }).ToList(),
                        Video = lesson.Video,

                    };
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public IEnumerable<CoursePerStudentListView> GetAll(string userId)
        {
            var result = new List<CoursePerStudentListView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var reviews = ctx.GetEntity<ReviewsPerCourse>();
                    var progressPerUser = ctx.GetEntity<CourseLessonLearnedsPerStudent>();
                    var lessons = ctx.GetEntity<LessonsPerCourse>();

                    /*
                     * Obtenemos los cursos que tiene un usuario, para hacerlo más
                     * fácil he agregado como propiedad a la clase Cursos la relación UsersPerCourse.
                     * De esta manera, puedo realizar la búsqueda por las propiedades.
                     */

                    var courses = _courseRepo.Find(x =>
                        x.Users.Any(y => y.UserId == userId)
                    ).OrderBy(x => x.Name).ToList();

                    result = courses.Select(x => new CoursePerStudentListView
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = x.Image1,
                        Description = x.Description,
                        /*
                          * El progreso se calcula mediante la siguiente fórmula:
                          * (Lecciones aprendidoa / Total de lecciones) * 100
                          */
                        Progress = Convert.ToInt32((
                            progressPerUser.Where(y => y.Lesson.CourseId == x.Id && y.UserId == userId).Count() /
                            Convert.ToDecimal(lessons.Where(y => y.CourseId == x.Id).Count())
                        ) * 100),
                        HaveVotes = reviews.Where(y => y.UserId == userId && y.CourseId == x.Id).Any(),
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper Review(ReviewsPerCourse model)
        {
            var rh = new ResponseHelper();

            try
            {
                // Registramos la valoración
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var hasReview = _reviewPerCourseRepo.Find(x =>
                        x.UserId == model.UserId
                        && x.CourseId == model.CourseId
                    ).Any();

                    if (!hasReview)
                    {
                        _reviewPerCourseRepo.Insert(model);

                        ctx.SaveChanges();
                        rh.SetResponse(true);
                    }
                    else
                    {
                        rh.SetResponse(false, "Usted ya ha valorado este curso");
                    }
                }

                if (rh.Response)
                {
                    // Calculamos el promedio de valoraciones
                    using (var ctx = _dbContextScopeFactory.Create())
                    {
                        var course = _courseRepo.Single(x =>
                            x.Id == model.CourseId
                        );

                        course.Vote = _reviewPerCourseRepo.Find(x =>
                            x.CourseId == model.CourseId
                        ).Average(x => x.Vote);

                        _courseRepo.Update(course);

                        ctx.SaveChanges();
                        rh.SetResponse(true);
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }

            return rh;
        }

        public ResponseHelper MarkAsLearned(int courseId, int lessonId, string userId)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    // Verificamos si existe en la base de datos
                    var originalEntry = _lessonLearnedRepo.SingleOrDefault(x =>
                        x.LessonId == lessonId
                        && x.UserId == userId
                    );

                    // Quiere decir que no existe, por lo tanto lo registramos
                    if (originalEntry == null)
                    {
                        _lessonLearnedRepo.Insert(new CourseLessonLearnedsPerStudent
                        {
                            UserId = userId,
                            LessonId = lessonId
                        });

                        rh.SetResponse(true);
                    }
                    // Por lo tanto ya existe
                    else
                    {
                        rh.SetResponse(false, "Usted ya marcó esta lección como aprendida");
                    }

                    ctx.SaveChanges();
                }

                #region Marcar como finalizado el curso
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    // Obtiene la relación entre el usuario y el curso
                    var coursePerUser = _userPerCourseRepo.Find(x => 
                        x.CourseId == courseId && x.UserId == userId
                    ).Single();

                    // Obtiene el total de lecciones para el curso
                    var totalLessons = _lessonPerCourseRepo.Find(x => x.CourseId == courseId)
                                                           .Count();

                    // Obtiene el total de lecciones aprendidas para el curso y por el usuario
                    var totalLearneds = _lessonLearnedRepo.Find(x =>
                        x.Lesson.CourseId == courseId
                        && x.UserId == userId
                    ).Count();

                    /*
                     * Si los cursos aprendidos con los que hay en total son la misma cantidad, entonces
                     * lo seteamos como finalizado
                     */
                    coursePerUser.Completed = totalLessons == totalLearneds;
                    _userPerCourseRepo.Update(coursePerUser);

                    ctx.SaveChanges();
                }
                #endregion
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                rh.SetResponse(false, e.Message);
            }

            return rh;
        }
    }
}
