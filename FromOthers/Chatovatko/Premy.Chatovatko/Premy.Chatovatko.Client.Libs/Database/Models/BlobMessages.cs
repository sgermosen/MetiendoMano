using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class BlobMessages
    {
        public BlobMessages()
        {
            ToSendMessages = new HashSet<ToSendMessages>();
        }

        public long Id { get; set; }
        public long? PublicId { get; set; }
        public long SenderId { get; set; }
        public long DoDelete { get; set; }
        public long Failed { get; set; }

        public Contacts Sender { get; set; }
        public Alarms Alarms { get; set; }
        public Contacts Contacts { get; set; }
        public Messages Messages { get; set; }
        public MessagesThread MessagesThread { get; set; }
        public ICollection<ToSendMessages> ToSendMessages { get; set; }
    }
}
