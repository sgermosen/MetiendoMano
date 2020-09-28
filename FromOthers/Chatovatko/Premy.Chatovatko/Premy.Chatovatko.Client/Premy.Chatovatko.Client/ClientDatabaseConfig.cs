using Premy.Chatovatko.Client.Libs.UserData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client
{
    public class ClientDatabaseConfig : IClientDatabaseConfig
    {
        public string DatabaseAddress => Utils.GetDatabaseAddress();
    }
}
