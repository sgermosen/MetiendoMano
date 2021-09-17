using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Contracts;
using Web.Core.Models;

namespace WebPage.Services
{
    /// <summary>
    /// Represent an object relational mapper (ORM) for the application.
    /// </summary>
    public class ApplicationDbContext : DbContext, IDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext() {}

        internal ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Default constructor for the application database context.
        /// </summary>
        /// <param name="options">database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region Entities

        /// <summary>
        /// Audio entity for the application.
        /// </summary>
        public DbSet<Audio> Audios { get; set; }

        #endregion

        #region Configuration

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audio>().ToTable("Audio");

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Audit



        #endregion

        #region Interface Methods

        /// <inheritdoc/>
        public bool CanConnect() => Database.CanConnect();

        /// <inheritdoc/>
        public void Migrate() => Database.Migrate();

        /// <inheritdoc/>
        public bool IsDataFetched()
        {
            try
            {
                var result = Audios.Any();
                return true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }

        /// <inheritdoc/>
        public Task<bool> EnsureCreated() => Database.EnsureCreatedAsync();

        /// <inheritdoc/>
        public Task<bool> EnsureDeleted() => Database.EnsureDeletedAsync();

        /// <inheritdoc/>
        public Task<int> FetchInitialData()
        {
            return SaveChangesAsync();
        }

        #endregion
    }
}
