using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact
{
    public class SearchCServerCapsula
    {
        public SearchCServerCapsula()
        {

        }

        public SearchCServerCapsula(IUser user)
        {
            if(user == null)
            {
                Succeeded = false;
                return;
            }
            UserId = user.Id;
            UserName = user.UserName;
            PemCertificate = user.PublicCertificate;
            Succeeded = true;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PemCertificate { get; set; }
        public bool Succeeded { get; set; }
    }
}
