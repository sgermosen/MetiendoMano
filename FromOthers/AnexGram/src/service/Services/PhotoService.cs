using Microsoft.AspNetCore.Hosting;
using Model.Domain;
using Model.Shared;
using Persistence.DatabaseContext;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public interface IPhotoService
    {
        Task<bool> Create(PhotoCreateDto model, FileDto file);
        Task<IEnumerable<PhotoListDto>> GetAll(ApiFilter filter);
    }

    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PhotoService(
            ApplicationDbContext context,
            IHostingEnvironment hostingEnvironment
        )
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<bool> Create(
            PhotoCreateDto model,
            FileDto file
        )
        {
            var result = false;

            try
            {
                model.Url = await UploadImage(file);

                _context.Photos.Add(new Photo
                {
                    Url = model.Url,
                    UserId = model.UserId
                });

                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        private async Task<string> UploadImage(FileDto model)
        {
            var fileName = model.UniqueName;
            var filePath = $"{_hostingEnvironment.WebRootPath}\\Uploads\\{fileName}";

            using (var ms = model.ReadAsStream())
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await ms.CopyToAsync(file);
            }

            return fileName;
        }

        public async Task<IEnumerable<PhotoListDto>> GetAll(ApiFilter filter)
        {
            var result = new List<PhotoListDto>();

            try
            {
                var _filter = new PhotoListFilter();

                if (!string.IsNullOrEmpty(filter.Filter))
                {
                    _filter = JsonConvert.DeserializeObject<PhotoListFilter>(filter.Filter);
                }

                var query = (
                    from p in _context.Photos
                    from u in _context.Users.Where(x => x.Id == p.UserId)
                    select new PhotoListDto
                    {
                        PhotoId = p.Id,
                        Comments = _context.CommentsPerPhoto.Count(x => x.PhotoId == p.Id),
                        Likes = _context.LikesPerPhoto.Count(x => x.PhotoId == p.Id),
                        Image = p.Url,
                        CreatedAt = p.CreatedAt,
                        UserId = p.UserId,
                        UserName = u.Name,
                        UserPic = u.Image,
                        UserSeoUrl = u.SeoUrl,
                        ILikedIt = _context.LikesPerPhoto.Any(x =>
                            x.UserId == _filter.UserId
                            && x.PhotoId == p.Id
                        )
                    }
                ).Take(filter.Take);

                // Nuestra condicion
                query = query.Where(x => _filter.SeoUrl == null || x.UserSeoUrl == _filter.SeoUrl);

                result = await query.ToListAsync();
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }
    }
}
