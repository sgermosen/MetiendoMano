using Premy.Chatovatko.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Premy.Chatovatko.Server.Database
{
    public class ConnectionInfo
    {
        public ConnectionInfo(Users user, X509Certificate2 cert, int clientId)
        {
            UserId = user.Id;
            UserName = user.UserName;
            Certificate = cert;
            ClientId = clientId;
        }

        public int UserId { get; }
        public string UserName { get; }
        public int ClientId { get; }
        public X509Certificate2 Certificate {get; }
    }
}
