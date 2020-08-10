using System.Threading.Tasks;
using Web.Core.Contracts;
using Web.Core.Models;

namespace WebPage.Services
{
    /// <summary>
    /// Represen a temporal in memory work holder, that can commit the work done to the persistence storage.
    /// </summary>
    public class UnitOfWork : DisposableBase, IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;

        /// <summary>
        /// Default constructor for the application unit of work.
        /// </summary>
        /// <param name="appDbContext">Application database context.</param>
        public UnitOfWork(ApplicationDbContext appDbContext) => _appDbContext = appDbContext;

        #region Interface Methods

        /// <inheritdoc/>
        public void Commit() => _appDbContext.SaveChanges();

        /// <inheritdoc/>
        public Task CommitAsync() => _appDbContext.SaveChangesAsync();

        /// <inheritdoc/>
        public IDataRepository<T> GetRepository<T>() where T : class, IEntity<int> => new DataRepository<T>(_appDbContext);

        #endregion
    }
}
