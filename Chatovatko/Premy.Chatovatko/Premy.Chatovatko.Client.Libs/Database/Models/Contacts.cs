using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class Contacts : IBlobbed
    {
        public Contacts()
        {
            BlobMessages = new HashSet<BlobMessages>();
            MessagesThread = new HashSet<MessagesThread>();
            ToSendMessages = new HashSet<ToSendMessages>();
        }

        public long PublicId { get; set; }
        public string UserName { get; set; }
        public string PublicCertificate { get; set; }
        public long Trusted { get; set; }
        public byte[] SendAesKey { get; set; }
        public byte[] ReceiveAesKey { get; set; }
        public long? BlobMessagesId { get; set; }
        public long AlarmPermission { get; set; }
        public string NickName { get; set; }

        public long GetBlobId()
        {
            return (long)BlobMessagesId;
        }

        public string ShowName
        {
            get
            {
                if (NickName == null || NickName == "")
                {
                    return UserName;
                }
                return NickName;
            }
        }

        public BlobMessages BlobMessagesNavigation { get; set; }
        public ICollection<BlobMessages> BlobMessages { get; set; }
        public ICollection<MessagesThread> MessagesThread { get; set; }
        public ICollection<ToSendMessages> ToSendMessages { get; set; }
    }
}
