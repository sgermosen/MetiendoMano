using System;
using System.Threading.Tasks;

namespace Web.Core.Contracts
{
    /// <summary>
    /// Represen a temporal in memory work holder, that can commit the work done to the persistence storage.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get the current repository for the unit of work.
        /// </summary>
        /// <typeparam name="T">Type of the working entity.</typeparam>
        /// <returns><see cref="IDataRepository{T}"/></returns>
        IDataRepository<T> GetRepository<T>() where T : class, IEntity<int>;

        /// <summary>
        /// Commit all work done to the persistence storage.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit all work done to the persistence storage asynchronously.
        /// </summary>
        Task CommitAsync();
    }
}
