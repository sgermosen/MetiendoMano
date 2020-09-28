using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.UserData
{
    public class SettingsCapsula
    {
        public Settings Settings { get; }
        public X509Certificate2 ClientCertificate { get; }
        public long UserPublicId => Settings.UserPublicId;
        public string PrivateCertificate => Settings.PrivateCertificate;
        public string UserName => Settings.UserName;
        public string ServerName => Settings.ServerName;
        public string ServerAddress => Settings.ServerAddress;
        public string ServerPublicCertificate => Settings.ServerPublicCertificate;
        public long ClientId => Settings.ClientId;
        public IClientDatabaseConfig Config { get; }

        public SettingsCapsula(Settings settings, IClientDatabaseConfig config)
        {
            Settings = settings;
            Config = config;
            ClientCertificate = new X509Certificate2(Convert.FromBase64String(settings.PrivateCertificate), String.Empty, X509KeyStorageFlags.Exportable);
        }

        
    }
}
