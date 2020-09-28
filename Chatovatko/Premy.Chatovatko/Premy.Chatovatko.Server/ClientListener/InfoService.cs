using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Premy.Chatovatko.Server.ClientListener
{
    public class InfoService : ILoggable
    {
        private readonly Logger logger;
        private readonly ServerConfig config;
        private readonly X509Certificate2 cert;

        public InfoService(Logger logger, ServerConfig config, X509Certificate2 cert)
        {
            this.logger = logger;
            this.config = config;
            this.cert = cert;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    logger.Log(this, "I have booted up.");
                    TcpListener listener = new TcpListener(IPAddress.Any, TcpConstants.INFO_SERVER_PORT);
                    listener.Start();
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        logger.Log(this, String.Format("Client {0} has connected.",
                            LUtils.GetIpAddress(client)));
                        Task.Run(() =>
                        {
                            try
                            {
                                using (Stream stream = client.GetStream()) {
                                    TextEncoder.SendJson(stream, GetServerInfo());
                                }
                                
                            }
                            catch (Exception exception)
                            {
                                logger.LogException(this, exception);
                            }
                            finally
                            {
                                client.Dispose();
                            }
                            
                        });
                    }
                }
                catch (Exception exception)
                {
                    logger.LogException(this, exception);
                }
            }
        }

        public ServerInfo GetServerInfo()
        {
            String publicKey = X509Certificate2Utils.ExportToPem(cert);
            return new ServerInfo(config.ServerName, publicKey, config.Password != null);
        }

        public void RunInBackground()
        {
            Task.Run(() => Run());

        }

        public string GetLogSource()
        {
            return "Information tcp service";
        }
    }
}
