using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class Clients
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public Users User { get; set; }
        public ICollection<ClientsMessagesDownloaded> ClientsMessagesDownloaded { get; set; }
    }
}
