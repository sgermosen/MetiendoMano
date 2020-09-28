using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class ClientsMessagesDownloaded
    {
        public int Id { get; set; }
        public int ClientsId { get; set; }
        public int BlobMessagesId { get; set; }

        public BlobMessages BlobMessages { get; set; }
        public Clients Clients { get; set; }
    }
}
