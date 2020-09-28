using Model.Shared;
using Persistence.DatabaseContext;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface ICommentService
    {
        Task<bool> Create(CommentCreateDto model);
        Task<IEnumerable<CommentListDto>> GetAll(ApiFilter filter);
    }

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService (
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<bool> Create(
            CommentCreateDto model
        )
        {
            var result = false;

            try
            {
                _context.CommentsPerPhoto.Add(new CommentsPerPhoto
                {
                    PhotoId = model.PhotoId,
                    UserId = model.UserId,
                    Comment = model.Comment.Trim()
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // Error logging
            }

            return result;
        }

        public async Task<IEnumerable<CommentListDto>> GetAll(ApiFilter filter)
        {
            var result = new List<CommentListDto>();

            try
            {
                var _filter = new CommentListFilter();

                if (!string.IsNullOrEmpty(filter.Filter))
                {
                    _filter = JsonConvert.DeserializeObject<CommentListFilter>(filter.Filter);
                }

                var query = (
                    from c in _context.CommentsPerPhoto
                    from u in _context.Users.Where(x => x.Id == c.UserId)
                    select new CommentListDto
                    {
                        CommentId = c.Id,
                        Comment = c.Comment,
                        PhotoId = c.PhotoId,
                        User = u.Name,
                        CreatedAt = c.CreatedAt
                    }
                ).Take(filter.Take);

                // Los parámetros del filtro todos deberían ser opcionales
                query = query.Where(x => _filter.PhotoId == null || x.PhotoId == _filter.PhotoId);

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
