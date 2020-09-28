using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class MessagesThread : IBlobbed
    {
        public MessagesThread()
        {
            Messages = new HashSet<Messages>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Onlive { get; set; }
        public long Archived { get; set; }
        public long WithUser { get; set; }
        public long BlobMessagesId { get; set; }
        public long PublicId { get; set; }

        public BlobMessages BlobMessages { get; set; }
        public Contacts WithUserNavigation { get; set; }
        public ICollection<Messages> Messages { get; set; }

        public long GetBlobId()
        {
            return BlobMessagesId;
        }
    }
}
