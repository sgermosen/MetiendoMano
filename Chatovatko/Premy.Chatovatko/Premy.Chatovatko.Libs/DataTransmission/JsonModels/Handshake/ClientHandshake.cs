using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Handshake
{
    public class ClientHandshake
    {
        public string UserName { get; set; }
        public string PemCertificate { get; set; }
        public int? ClientId { get; set; }
        public string ServerPassword { get; set; }
    }
}
