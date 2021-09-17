namespace Notif.Backend.Data.Repositories.Contracts
{
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IReaction : IRepository<Reaction>
    {
        IQueryable GetAllWithUsers();
        Task<IQueryable<Reaction>> GetReactionsAsync(string userName);

        Task CreateReactionAsync(Reaction model, string userName);
        //IEnumerable<SelectListItem> GetComboProducts();
    }
}
