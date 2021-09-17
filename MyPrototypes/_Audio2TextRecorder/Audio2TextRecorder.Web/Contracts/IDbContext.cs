using System;
using System.Threading;
using System.Threading.Tasks;

namespace Audio2TextRecorder.Web.Contracts
{
    /// <summary>
    /// Represent an object relational mapper (ORM) for the application.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Indicates weather the database context cant connect or not.
        /// </summary>
        /// <returns>True or False</returns>
        bool CanConnect();

        /// <summary>
        /// Save all uncommitted changes.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        int SaveChanges();

        /// <summary>
        /// Save all uncommitted changes asynchronously.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// If the data base does not exits is created. If exits does nothing and returns false.
        /// </summary>
        /// <returns><see langword="true"/>/<see langword="false"/></returns>
        Task<bool> EnsureCreated();

        /// <summary>
        /// If the data base does exits is deleted. If not exits does nothing and returns false.
        /// </summary>
        /// <returns><see langword="true"/>/<see langword="false"/></returns>
        Task<bool> EnsureDeleted();

        /// <summary>
        /// Apply any pending migration to the database. Will create the database if do not already exits.
        /// </summary>
        void Migrate();

        /// <summary>
        /// Indicates weather or not the database has been fetched.
        /// </summary>
        /// <returns>True/False</returns>
        bool IsDataFetched();

        /// <summary>
        /// Fetch default data to the database.
        /// </summary>
        /// <returns>Count of affected rows.</returns>
        Task<int> FetchInitialData();
    }
}
