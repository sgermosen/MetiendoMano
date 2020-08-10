using System;
using Microsoft.EntityFrameworkCore;
using Notif.Backend.Models;
using System.Linq;
using System.Threading.Tasks;
using Notif.Backend.Data.Repositories.Contracts;
using Notif.Backend.Helpers;

namespace Notif.Backend.Data.Repositories.Implementations
{
    public class ReactionRepository : Repository<Reaction>, IReaction
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ReactionRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Reactions.Include(p => p.ApplicationUser);
        }

        public async Task CreateReactionAsync(Reaction model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }
            model.ApplicationUser = user;
            model.Private = false;
            //model.Date=DateTime.UtcNow;

            _context.Reactions.Add(model);
            await _context.SaveChangesAsync();
            
        }

        public async Task<IQueryable<Reaction>> GetReactionsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Reactions
                    .Include(o => o.ApplicationUser)
                    .OrderByDescending(o => o.Date);
            }

            return _context.Reactions
                .Where(o => o.ApplicationUser == user)
                .OrderByDescending(o => o.Date);
        }

        //public IEnumerable<SelectListItem> GetComboProducts()
        //{
        //    var list = _context.Products.Select(p => new SelectListItem
        //    {
        //        Text = p.Name,
        //        Value = p.Id.ToString()
        //    }).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Select a product...)",
        //        Value = "0"
        //    });

        //    return list;
        //}
    }
}
