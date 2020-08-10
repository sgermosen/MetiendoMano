using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Contracts
{
    /// <summary>
    /// Represent and data resource access for operations.
    /// </summary>
    /// <typeparam name="T">Type for the represented data.</typeparam>
    public interface IDataRepository<T> : IDisposable
        where T : class, IEntity<int>
    {
        /// <summary>
        /// Represent a queryable object to execute query operations.
        /// </summary>
        /// <returns><see cref="IQueryable"/>&lt;<typeparamref name="T"/>&gt;</returns>
        IQueryable<T> DataBase { get; }

        #region GET

        /// <summary>
        /// Get the current <see cref="IUnitOfWork"/> for the repository.
        /// </summary>
        /// <returns><see cref="IUnitOfWork"/></returns>
        IUnitOfWork GetTransaction();

        /// <summary>
        /// Get the queried data, if not found get a null result.
        /// </summary>
        /// <param name="query">The query operation for the request.</param>
        /// <returns><typeparamref name="T"/></returns>
        T Get(Func<T, bool> query);

        /// <summary>
        /// Async operation to get the queried data, if not found get a null result.
        /// </summary>
        /// <param name="query">The query operation for the request.</param>
        /// <returns><see cref="Task{T}"/></returns>
        Task<T> GetAsync(Func<T, bool> query);

        /// <summary>
        /// Get the queried data, if not found get an empty list.
        /// </summary>
        /// <param name="query">The query operation for the request.</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> Query(Func<T, bool> query);

        /// <summary>
        /// Async operation to get the queried data, if not found get an empty list.
        /// </summary>
        /// <param name="query">The query operation for the request.</param>
        /// <returns><see cref="Task"/>&lt;<see cref="IEnumerable{T}"/>&gt;</returns>
        Task<IEnumerable<T>> QueryAsync(Func<T, bool> query);

        /// <summary>
        /// Indicate weather the repository has data or not <see cref="bool"/>.
        /// </summary>
        /// <returns>True/False</returns>
        bool Any(Func<T, bool> query = null);

        /// <summary>
        /// Async operation that indicates weather the repository has data or not.
        /// </summary>
        /// <returns><see cref="Task"/>&lt;<see cref="bool"/>&gt;</returns>
        Task<bool> AnyAsync(Func<T, bool> query = null);

        #endregion

        #region POST

        /// <summary>
        /// Add the given entity into the repository.
        /// </summary>
        /// <param name="entity">Entity to be inserted into the repository.</param>
        void Add(T entity);

        /// <summary>
        /// Add the given entity into the repository asynchronously.
        /// </summary>
        /// <param name="entity">Entity to be inserted into the repository.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Add a group of entity into the repository.
        /// </summary>
        /// <param name="entities">List of entities to be inserted into the repository.</param>
        void AddGroup(IEnumerable<T> entities);

        /// <summary>
        /// Add a group of entity into the repository asynchronously.
        /// </summary>
        /// <param name="entities">List of entities to be inserted into the repository.</param>
        Task AddGroupAsync(IEnumerable<T> entities);

        #endregion

        #region PUT/PATCH

        /// <summary>
        /// Update the selected entity in the repository if exists.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        void Update(T entity);

        /// <summary>
        /// Update the selected entity in the repository asynchronously if exists.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Update the selected entities in the repository using a query selection.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="query">Query to selected the entities to be updated.</param>
        void UpdateGroup(T entity, Func<T, bool> query);

        /// <summary>
        /// Update the selected entities in the repository asynchronously using a query selection.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="query">Query to selected the entities to be updated.</param>
        Task UpdateGroupAsync(T entity, Func<T, bool> query);

        #endregion

        #region DELETE

        /// <summary>
        /// Delete the selected entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Delete the selected entity in the repository asynchronously.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Delete the selected entities by using a query selection.
        /// </summary>
        /// <param name="query">Query to selected the entities to be deleted.</param>
        void DeleteGroup(Func<T, bool> query);

        /// <summary>
        /// Delete the selected entities by using a query selection asynchronously.
        /// </summary>
        /// <param name="query">Query to selected the entities to be deleted.</param>
        Task DeleteGroupAsync(Func<T, bool> query);

        #endregion
    }
}
