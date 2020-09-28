using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.ClientCommunication
{
    public static class ConnectionUtils
    {
        public static void Register(out Connection connection, out SettingsCapsula settings, Logger logger, Action<String> log, 
            String serverAddress, X509Certificate2 clientCert, IClientDatabaseConfig config, 
            String userName, SettingsLoader settingsLoader, ServerInfo info)
        {
            IConnectionVerificator verificator = new ConnectionVerificator(logger, info.PublicCertificate);
            connection = new Connection(logger, verificator, serverAddress, clientCert, config, userName);
            connection.Connect();

            log("Saving settings.");
            settingsLoader.Create(clientCert, connection.UserId, connection.UserName, info.Name, serverAddress, info.PublicCertificate, (int)connection.ClientId);
            settings = settingsLoader.GetSettingsCapsula();

            log("Saving the self AES key.");
            //The only user outside of the chain
            using (Context context = new Context(config))
            {
                context.Contacts.Add(new Contacts()
                {
                    PublicId = connection.UserId,
                    UserName = connection.UserName,
                    AlarmPermission = 1,
                    BlobMessagesId = null,

                    NickName = null,
                    Trusted = 1,
                    ReceiveAesKey = connection.SelfAesPassword?.Password,
                    SendAesKey = connection.SelfAesPassword?.Password,

                    PublicCertificate = X509Certificate2Utils.ExportToPem(clientCert)
                });
                context.SaveChanges();
            }

            log("Self-trustification begin.");
            connection.TrustContact(connection.UserId);
            log("Self-trustification done.");

            log("Updating.");
            connection.Pull();
            connection.Push();
            log("Updating done.");
        }
    }
}
