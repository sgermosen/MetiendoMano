using Audio2TextRecorder.Web.Contracts;
using Audio2TextRecorder.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Services
{
    /// <summary>
    /// Represent and data resource access for operations.
    /// </summary>
    /// <typeparam name="T">Type for the represented data.</typeparam>
    public class DataRepository<T> : DisposableBase, IDataRepository<T> where T : class, IEntity<int>
    {
        private readonly ApplicationDbContext _appDbContext;

        /// <summary>
        /// Default constructor for the repository.
        /// </summary>
        /// <param name="appDbContext">Application database context for the repository.</param>
        public DataRepository(ApplicationDbContext appDbContext) => _appDbContext = appDbContext;

        /// <inheritdoc/>
        public IQueryable<T> DataBase => _appDbContext.Set<T>();

        #region Queries

        #region GET

        /// <inheritdoc/>
        public IUnitOfWork GetTransaction() => new UnitOfWork(_appDbContext);

        /// <inheritdoc/>
        public T Get(Func<T, bool> query) => _appDbContext.Set<T>().FirstOrDefault(query);

        /// <inheritdoc/>
        public Task<T> GetAsync(Func<T, bool> query) => Task.FromResult(Get(query));

        /// <inheritdoc/>
        public IEnumerable<T> Query(Func<T, bool> query) => _appDbContext.Set<T>().Where(query).ToList();

        /// <inheritdoc/>
        public Task<IEnumerable<T>> QueryAsync(Func<T, bool> query) => Task.FromResult(Query(query));

        #endregion

        #region OPTIONS

        /// <inheritdoc/>
        public bool Any(Func<T, bool> query = null)
        {
            if (query != null)
                return _appDbContext.Set<T>().Any(query);

            return _appDbContext.Set<T>().Any();
        }

        /// <inheritdoc/>
        public Task<bool> AnyAsync(Func<T, bool> query = null)
        {
            if (query != null)
                return Task.FromResult(Any(query));

            return Task.FromResult(Any());
        }

        #endregion

        #endregion

        #region Commands

        #region POST

        /// <inheritdoc/>
        public void Add(T entity) => _appDbContext.Set<T>().Add(entity);

        /// <inheritdoc/>
        public Task AddAsync(T entity) => Task.Run(() => Add(entity));

        /// <inheritdoc/>
        public void AddGroup(IEnumerable<T> entities) => _appDbContext.Set<T>().AddRange(entities);

        /// <inheritdoc/>
        public Task AddGroupAsync(IEnumerable<T> entities) => Task.Run(() => AddGroup(entities));

        #endregion


        #region DELETE

        /// <inheritdoc/>
        public void Delete(T entity) => _appDbContext.Set<T>().Remove(entity);

        /// <inheritdoc/>
        public Task DeleteAsync(T entity) => Task.Run(() => Delete(entity));

        /// <inheritdoc/>
        public void DeleteGroup(Func<T, bool> query) => _appDbContext.Set<T>().RemoveRange(Query(query));

        /// <inheritdoc/>
        public Task DeleteGroupAsync(Func<T, bool> query) => Task.Run(() => DeleteGroup(query));

        #endregion


        #region PUT/PACH

        /// <inheritdoc/>
        public void Update(T entity) => _appDbContext.Set<T>().Update(entity);

        /// <inheritdoc/>
        public Task UpdateAsync(T entity) => Task.Run(() => Update(entity));

        /// <inheritdoc/>
        public void UpdateGroup(T entity, Func<T, bool> query)
        {
            var entitiesToUpdate = Query(query);
            var updatedEntities = entitiesToUpdate.Select(x => UpdateGenericObject(x, entity));
            _appDbContext.Set<T>().UpdateRange(updatedEntities);
        }

        /// <inheritdoc/>
        public Task UpdateGroupAsync(T entity, Func<T, bool> query) => Task.Run(() => UpdateGroup(entity, query));

        #endregion

        #endregion

        #region Auxiliary Methods

        /// <summary>
        /// Updates the selected entity with the given changes of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated.</param>
        /// <param name="entityChanges">Changes to apply to the entity.</param>
        /// <returns><typeparamref name="T"/></returns>
        private T UpdateGenericObject(T entityToUpdate, T entityChanges)
        {
            var toUpdateProperties = entityToUpdate.GetType().GetProperties();
            var changedPropertyInfos = entityChanges.GetType().GetProperties();

            for (var i = 0; i < toUpdateProperties.Length; i++)
            {
                if (toUpdateProperties[i].Name == nameof(entityChanges.Id))
                    continue;
                toUpdateProperties[i].SetValue(entityToUpdate,
                    changedPropertyInfos[i].GetValue(entityChanges));
            }

            return entityToUpdate;
        }

        #endregion
    }
}
