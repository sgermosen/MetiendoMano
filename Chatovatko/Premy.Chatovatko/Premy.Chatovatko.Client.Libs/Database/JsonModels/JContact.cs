using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public class JContact : IJType
    {

        public long PublicId { get; set; }
        public string UserName { get; set; }
        public string PublicCertificate { get; set; }
        public bool Trusted { get; set; }

        public byte[] SendAesKey { get; set; }
        public byte[] ReceiveAesKey { get; set; }
        public bool AlarmPermission { get; set; }
        public string NickName { get; set; }

        public JsonTypes GetJsonType()
        {
            return JsonTypes.CONTACT;
        }

        public long GetPriority()
        {
            return 100;
        }
    }
}
