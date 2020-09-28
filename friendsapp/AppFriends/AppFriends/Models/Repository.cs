using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFriends.Models
{
    public class Repository
    {
        // Obtendo os dados de back-end Azure Mobile Apps;
        public async Task<List<Friend>> GetFriends()
        {
            var service = new Services.AzureService<Friend>();
            var items = await service.GetTable();

            return items.ToList();
        }
    }
}
