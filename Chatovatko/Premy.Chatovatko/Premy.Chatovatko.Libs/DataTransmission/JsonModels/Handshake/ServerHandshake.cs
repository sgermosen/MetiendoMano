using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Handshake
{
    public class ServerHandshake
    {
        public bool NewUser { get; set; }
        public bool Succeeded { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Errors { get; set; }
        public int ClientId { get; set; }
        public byte[] SelfAesKey { get; set; }
    }
}
