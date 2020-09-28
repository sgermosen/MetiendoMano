using Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Model.Domain.DbHelper;
using Persistence.DatabaseContext.Config;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserFactory _currentUser;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserFactory currentUser = null
        ) : base(options)
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // My custom filters
            AddMyFilters(ref modelBuilder);

            // My fluent api contraints
            new ApplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
            new CommentsPerPhotoConfig(modelBuilder.Entity<CommentsPerPhoto>());
            new LikesPerPhotoConfig(modelBuilder.Entity<LikesPerPhoto>());
            new PhotoConfig(modelBuilder.Entity<Photo>());
        }

        public DbSet<CommentsPerPhoto> CommentsPerPhoto { get; set; }
        public DbSet<LikesPerPhoto> LikesPerPhoto { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is AuditEntity
                    && (
                    x.State == EntityState.Added
                    || x.State == EntityState.Modified
                )
            );

            var user = new CurrentUser();

            if (_currentUser != null)
            {
                user = _currentUser.Get;
            }

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditEntity;

                if (entity != null)
                {
                    var date = DateTime.Now;
                    string userId = user.UserId;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = date;
                        entity.CreatedBy = userId;
                    }
                    else if (entity is ISoftDeleted && ((ISoftDeleted)entity).Deleted)
                    {
                        entity.DeletedAt = date;
                        entity.DeletedBy = userId;
                    }

                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                    entity.UpdatedAt = date;
                    entity.UpdatedBy = userId;
                }
            }
        }

        private void AddMyFilters(ref ModelBuilder modelBuilder)
        {
            #region SoftDeleted
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<CommentsPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<LikesPerPhoto>().HasQueryFilter(x => !x.Deleted);
            modelBuilder.Entity<Photo>().HasQueryFilter(x => !x.Deleted);
            #endregion
        }
    }
}
