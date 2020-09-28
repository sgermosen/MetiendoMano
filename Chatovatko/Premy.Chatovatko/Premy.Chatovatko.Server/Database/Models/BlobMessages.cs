using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class BlobMessages
    {
        public int Id { get; set; }
        public int RecepientId { get; set; }
        public int SenderId { get; set; }
        public byte[] Content { get; set; }
        public int Priority { get; set; }

        public Users Recepient { get; set; }
        public Users Sender { get; set; }
        public ICollection<ClientsMessagesDownloaded> ClientsMessagesDownloaded { get; set; }
    }
}
