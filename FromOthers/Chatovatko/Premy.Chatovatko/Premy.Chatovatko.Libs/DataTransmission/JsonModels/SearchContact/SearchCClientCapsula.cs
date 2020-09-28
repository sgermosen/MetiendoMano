using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact
{
    public class SearchCClientCapsula
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public byte[] CertificateHash { get; set; }
    }
}
