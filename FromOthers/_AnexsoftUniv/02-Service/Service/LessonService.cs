using Common;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Service
{
    public interface ILessonService
    {
        IEnumerable<LessonsPerCourse> GetAll(int id);
        ResponseHelper Insert(LessonsPerCourse model);
        ResponseHelper Update(LessonsPerCourse model);
        ResponseHelper Delete(int id);
        ResponseHelper Order(List<LessonsPerCourse> model);
        LessonsPerCourse Get(int id);
    }

    public class LessonService : ILessonService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<LessonsPerCourse> _lessonRepo;

        public LessonService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<LessonsPerCourse> lessonRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _lessonRepo = lessonRepo;
        }

        public ResponseHelper Insert(LessonsPerCourse model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    // New order
                    var order = _lessonRepo.Find(x => x.CourseId == model.CourseId)
                                            .Select(x => x.Order)
                                            .DefaultIfEmpty(0).Max() + 1;

                    model.Order = order;
                    model.Content = string.Format("Contenido para la lección {0}", model.Name);

                    _lessonRepo.Insert(model);
                    ctx.SaveChanges();

                    rh.SetResponse(true);
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

        public ResponseHelper Update(LessonsPerCourse model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var entryOriginal = _lessonRepo.Single(x => x.Id == model.Id);

                    entryOriginal.Name = model.Name;
                    entryOriginal.Content = model.Content;
                    entryOriginal.Video = model.Video;

                    _lessonRepo.Update(entryOriginal);

                    ctx.SaveChanges();

                    rh.SetResponse(true);
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

        public ResponseHelper Order(List<LessonsPerCourse> model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var ids = model.Select(x => x.Id);
                    var originalEntries = _lessonRepo.Find(x => ids.Contains(x.Id)).ToList();

                    foreach (var e in originalEntries)
                    {
                        var newOrder = model.Find(x => x.Id == e.Id).Order;
                        e.Order = newOrder;

                        _lessonRepo.Update(e);
                    }

                    ctx.SaveChanges();

                    rh.SetResponse(true);
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

        public ResponseHelper Delete(int id)
        {
            var result = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var originalEntry = _lessonRepo.Single(x => x.Id == id);
                    _lessonRepo.Delete(originalEntry);

                    ctx.SaveChanges();
                    result.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public IEnumerable<LessonsPerCourse> GetAll(int courseId)
        {
            var result = new List<LessonsPerCourse>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _lessonRepo.Find(x => x.CourseId == courseId).OrderBy(x =>
                        x.CreatedAt
                    ).OrderBy(x => x.Order).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public LessonsPerCourse Get(int id)
        {
            var result = new LessonsPerCourse();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _lessonRepo.SingleOrDefault(x => x.Id == id);
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
