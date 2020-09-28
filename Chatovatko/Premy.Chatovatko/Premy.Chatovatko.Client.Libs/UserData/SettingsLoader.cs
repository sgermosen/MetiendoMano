using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Premy.Chatovatko.Libs.Cryptography;

namespace Premy.Chatovatko.Client.Libs.UserData
{
    public class SettingsLoader : ILoggable
    {
        private readonly Logger logger;
        private readonly IClientDatabaseConfig config;
        public SettingsLoader(IClientDatabaseConfig config, Logger logger)
        {
            this.logger = logger;
            this.config = config;
        }

        public bool Exists()
        {
            using(Context context = new Context(config))
            {
                var loaded = context.Settings.FirstOrDefault();
                return loaded != null;
            }
        }

        public SettingsCapsula GetSettingsCapsula()
        {
            using (Context context = new Context(config))
            {
                return new SettingsCapsula(context.Settings.First(), config);
            }
        }

        public void Create(X509Certificate2 clientCert, int userId, String userName, String serverName, String serverAddress, String serverPublicKey, long clientId)
        {
            if (Exists())
            {
                throw new Exception("Settings already exists");
            }
            Settings settings = new Settings()
            {
                UserPublicId = userId,
                PrivateCertificate = X509Certificate2Utils.ExportToBase64(clientCert),
                ServerAddress = serverAddress,
                ServerName = serverName,
                ServerPublicCertificate = serverPublicKey,
                UserName = userName,
                ClientId = clientId,
                LastUniqueId = clientId << 26
            };

            using (Context context = new Context(config))
            {
                context.Settings.Add(settings);
                context.SaveChanges();
            }
            
        }

        public string GetLogSource()
        {
            return "Settings creator and loader";
        }
    }
}
