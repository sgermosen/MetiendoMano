using Persistence.DatabaseContext;
using System.Data.Entity;
using System.Linq;

namespace Persistence.DbContextScope.Extensions
{
    public static class GetDbSet
    {
        public static DbSet<T> GetEntity<T>(this IDbContextReadOnlyScope value) where T : class
        {
            return value.DbContexts.Get<ApplicationDbContext>().Set<T>();
        }

        public static DbSet<T> GetEntity<T>(this IDbContextScope value) where T : class
        {
            return value.DbContexts.Get<ApplicationDbContext>().Set<T>();
        }

        public static int ExecuteCommand(
            this IDbContextReadOnlyScope ctx,
            string query,
            params object[] parameters
        )
        {
            return ctx.DbContexts.Get<ApplicationDbContext>().Database.ExecuteSqlCommand(query, parameters);
        }

        public static int ExecuteCommand(
            this IDbContextScope ctx,
            string query,
            params object[] parameters
        )
        {
            return ctx.DbContexts.Get<ApplicationDbContext>().Database.ExecuteSqlCommand(query, parameters);
        }

        public static IQueryable<T> SqlQuery<T>(
            this IDbContextReadOnlyScope ctx,
            string query,
            params object[] parameters
        )
        {
            return ctx.DbContexts.Get<ApplicationDbContext>().Database.SqlQuery<T>(query, parameters).AsQueryable();
        }

        public static IQueryable<T> SqlQuery<T>(
            this IDbContextScope ctx,
            string query,
            params object[] parameters
        )
        {
            return ctx.DbContexts.Get<ApplicationDbContext>().Database.SqlQuery<T>(query, parameters).AsQueryable();
        }
    }
}
