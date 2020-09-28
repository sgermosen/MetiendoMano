using Common.CustomFilters;
using Persistence.DatabaseContext;
using Persistence.DbContextScope;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public Repository(IAmbientDbContextLocator context)
        {
            _ambientDbContextLocator = context;
        }

        private ApplicationDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<ApplicationDbContext>();

                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");
                }

                return dbContext;
            }
        }

        private IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties,
                                                       IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #region IRepository<T> Members
        public IQueryable<T> AsQueryable()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            return PerformInclusions(includeProperties, query);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.Where(where);
        } 

        public T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.Single(where);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.SingleOrDefault(where);
        }

        public T First(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.First(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.FirstOrDefault(where);
        }

        public void Delete(T entity)
        {
            if (entity is ISoftDeleted)
            {
                ((ISoftDeleted)entity).Deleted = true;

                DbContext.Set<T>().Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                DbContext.Set<T>().Remove(entity);
            }
        }

        public void Insert(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var e in entities)
            {
                DbContext.Entry(e).State = EntityState.Added;
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var e in entities)
            {
                DbContext.Entry(e).State = EntityState.Modified;
            }
        }
        #endregion

        #region SQL Queries
        public virtual IQueryable<T> SelectQuery(string query, params object[] parameters)
        {
            return DbContext.Set<T>().SqlQuery(query, parameters).AsQueryable();
        }

        public virtual int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public IQueryable<I> ExecuteSqlCommand<I>(string query, params object[] parameters) where I : class
        {
            return DbContext.Database.SqlQuery<I>(query, parameters).AsQueryable();
        }
        #endregion
    }
}