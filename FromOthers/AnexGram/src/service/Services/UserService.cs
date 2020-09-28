using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Shared;
using Newtonsoft.Json;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<UserDto> Get(string id);
        Task<UserDto> GetByFilter(ApiFilter filter);
        Task<IEnumerable<UserDto>> GetAll(ApiFilter filter);
        Task<bool> PartialUpdate(string id, UserPartialDto model);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAll(ApiFilter filter)
        {
            var result = new List<UserDto>();

            try
            {
                var query = _context.Users.AsQueryable();

                var _filter = new UserListFilter();

                if (!string.IsNullOrEmpty(filter.Filter))
                {
                    _filter = JsonConvert.DeserializeObject<UserListFilter>(filter.Filter);
                }

                // Registros a limitar
                query.Take(filter.Take);

                // Condiciones
                query = query.Where(x => _filter.Name == null || (x.Name + "" + x.Lastname).Contains(_filter.Name));

                // Ordenamiento
                if (!string.IsNullOrEmpty(filter.Sort))
                {
                    if (filter.Sort.ToLower().Equals("name"))
                    {
                        if(!filter.Descending) query = query.OrderBy(x => x.Name);
                        else query = query.OrderByDescending(x => x.Name);
                    }
                }

                // Iteramos para mapear el automapper
                foreach (var user in await query.ToListAsync())
                {
                    result.Add(
                        Mapper.Map<UserDto>(user)
                    );
                }
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        public async Task<UserDto> GetByFilter(ApiFilter filter)
        {
            var result = new UserDto();

            try
            {
                var query = _context.Users.AsQueryable();

                var _filter = new UserGetFilter();

                if (!string.IsNullOrEmpty(filter.Filter))
                {
                    _filter = JsonConvert.DeserializeObject<UserGetFilter>(filter.Filter);
                }

                // Condiciones
                query = query.Where(x => _filter.UserId == null || x.Id == _filter.UserId);
                query = query.Where(x => _filter.SeoUrl == null || x.SeoUrl == _filter.SeoUrl);

                result = Mapper.Map<UserDto>(
                    await query.SingleAsync()
                );
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        public async Task<UserDto> Get(string id)
        {
            var result = new UserDto();

            try
            {
                var user = await _context.Users.SingleAsync(x => x.Id == id);
                result = Mapper.Map<UserDto>(user);
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        public async Task<bool> PartialUpdate(string id, UserPartialDto model)
        {
            var result = false;

            try
            {
                var user = await _context.Users.SingleAsync(x => x.Id == id);

                if (model.Name != null)
                    user.Name = model.Name;

                if (model.Lastname != null)
                    user.Lastname = model.Lastname;

                if (model.AboutUs != null)
                    user.AboutUs = model.AboutUs;

                if (model.Image != null)
                    user.Image = model.Image;

                _context.Update(user);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }
    }
}
