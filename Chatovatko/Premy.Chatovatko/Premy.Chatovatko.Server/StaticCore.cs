using Premy.Chatovatko.Server.Database;
using Premy.Chatovatko.Server.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Server
{
    public static class StaticCore
    {
        public static DBPool dbPool = null;
        public static IServerLogger logger = null;
        public static ServerConfig config = null;
    }
}
