using Microsoft.AspNetCore.Hosting;
using Model.Shared;
using Persistence.DatabaseContext;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Model.Domain;

namespace Services
{
    public interface ILikeService
    {
        Task<bool> Create(LikeCreateDto model);
        Task<bool> Remove(int id);
    }

    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LikeService (
            ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment
        )
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<bool> Create(
            LikeCreateDto model
        )
        {
            var result = false;

            try
            {
                var exists = await _context.LikesPerPhoto.AnyAsync(x => x.UserId == model.UserId && x.PhotoId == model.PhotoId);

                if (!exists)
                {
                    _context.LikesPerPhoto.Add(new LikesPerPhoto
                    {
                        PhotoId = model.PhotoId,
                        UserId = model.UserId
                    });

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        public async Task<bool> Remove(
            int id
        )
        {
            var result = false;

            try
            {
                var originalEntry = await _context.LikesPerPhoto.SingleOrDefaultAsync(x =>
                    x.Id == id
                );

                /*
                 * Aplicamos softdeleted para hacer borrado solo a nivel lógico.
                 * En nuestro ApplicationDbContext aplicamos filtros, estos excluirán
                 * cualquier consulta donde el Deleted = True
                 */

                if (originalEntry != null)
                {
                    originalEntry.Deleted = true;
                    _context.Update(originalEntry);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }
    }
}
