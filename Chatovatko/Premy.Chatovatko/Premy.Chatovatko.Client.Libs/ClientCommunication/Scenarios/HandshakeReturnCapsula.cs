using Premy.Chatovatko.Client.Libs.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.ClientCommunication.Scenarios
{
    public class HandshakeReturnCapsula
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public AESPassword SelfAesPassword { get; set; }
    }

}
