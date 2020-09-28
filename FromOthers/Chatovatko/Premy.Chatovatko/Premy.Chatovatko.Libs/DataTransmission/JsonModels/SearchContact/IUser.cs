using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact
{
    public interface IUser
    {
        int Id { get; set; }
        string PublicCertificate { get; set; }
        string UserName { get; set; }
    }
}
