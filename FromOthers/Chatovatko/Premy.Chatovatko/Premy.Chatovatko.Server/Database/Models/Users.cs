using Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact;
using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Server.Database.Models
{
    public partial class Users : IUser
    {
        public Users()
        {
            BlobMessagesRecepient = new HashSet<BlobMessages>();
            BlobMessagesSender = new HashSet<BlobMessages>();
            UsersKeysRecepient = new HashSet<UsersKeys>();
            UsersKeysSender = new HashSet<UsersKeys>();
        }

        public int Id { get; set; }
        public string PublicCertificate { get; set; }
        public byte[] PublicCertificateSha256 { get; set; }
        public string UserName { get; set; }

        public ICollection<BlobMessages> BlobMessagesRecepient { get; set; }
        public ICollection<BlobMessages> BlobMessagesSender { get; set; }
        public ICollection<UsersKeys> UsersKeysRecepient { get; set; }
        public ICollection<UsersKeys> UsersKeysSender { get; set; }
        public ICollection<Clients> Clients { get; set; }
    }
}
