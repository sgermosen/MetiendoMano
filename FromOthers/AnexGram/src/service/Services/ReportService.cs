using Model.Shared;
using Persistence.DatabaseContext;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services
{
    public interface IReportService
    {
        Task<DataGridResponse> GreaterUsersParticipation(DataGrid grid);
    }

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(
            ApplicationDbContext context
        )
        {
            _context = context;
        }

        public async Task<DataGridResponse> GreaterUsersParticipation(DataGrid grid)
        {
            grid.Initialize();

            try
            {
                var query = (
                    from u in _context.Users
                    select new {
                        User = $"{u.Lastname}, {u.Name}",
                        Likes = _context.LikesPerPhoto.Count(x => x.UserId == u.Id),
                        Comments = _context.CommentsPerPhoto.Count(x => x.UserId == u.Id),
                        Photos = _context.Photos.Count(x => x.UserId == u.Id)
                    }
                ).Select(x => new GreaterUsersParticipationDto
                {
                    User = x.User,
                    Likes = x.Likes,
                    Comments = x.Comments,
                    Photos = x.Photos,
                    // Esta es una pequeña lógica para generar un score a cada usuario
                    // En el cual mayor prioridad le doy a los que suben fotos y dan like
                    Score = Convert.ToDecimal(x.Photos * 0.5 + x.Likes * 0.3 + x.Comments * 0.2)
                }).OrderByDescending(x => x.Score).AsQueryable();

                var data = await query.Skip(grid.Page)
                                .Take(grid.RowsPerPage)
                                .ToListAsync();

                var total = await query.CountAsync();

                grid.SetData(data, total);
            }
            catch (Exception e)
            {
                // Error logging
            }

            return grid.Response();
        }
    }
}
