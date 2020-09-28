using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels
{
    public class ServerInfo
    {
        public ServerInfo(String name, String publicKey, bool passwordRequired)
        {
            this.Name = name;
            this.PublicCertificate = publicKey;
            this.PasswordRequired = passwordRequired;
        }
        public String Name { get; set; }
        public String PublicCertificate { get; set; }
        public bool PasswordRequired { get; set; }
    }
}
