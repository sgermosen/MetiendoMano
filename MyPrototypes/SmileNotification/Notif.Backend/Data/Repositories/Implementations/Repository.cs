using Microsoft.EntityFrameworkCore;
using Notif.Backend.Data.Entities;
using Notif.Backend.Data.Repositories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Notif.Backend.Data.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(string user)
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        //public async Task CreateAsync(T entity)
        //{
        //    await _context.Set<T>().AddAsync(entity);
        //    await SaveAllAsync();
        //}

        //public async Task UpdateAsync(T entity)
        //{
        //    _context.Set<T>().Update(entity);
        //    await SaveAllAsync();
        //}
        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }


        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
