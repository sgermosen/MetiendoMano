using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class ToSendMessages
    {
        public long Id { get; set; }
        public long RecepientId { get; set; }
        public byte[] Blob { get; set; }
        public long? BlobMessagesId { get; set; }
        public long Priority { get; set; }

        public BlobMessages BlobMessages { get; set; }
        public Contacts Recepient { get; set; }
    }
}
