using System;
using System.Collections.Generic;

namespace Premy.Chatovatko.Client.Libs.Database.Models
{
    public partial class Settings
    {
        public long Id { get; set; }
        public long UserPublicId { get; set; }
        public string PrivateCertificate { get; set; }
        public string UserName { get; set; }
        public string ServerName { get; set; }
        public string ServerAddress { get; set; }
        public string ServerPublicCertificate { get; set; }
        public long LastUniqueId { get; set; }
        public long ClientId { get; set; }
    }
}
