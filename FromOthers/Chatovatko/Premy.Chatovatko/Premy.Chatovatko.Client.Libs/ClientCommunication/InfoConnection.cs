using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.ClientCommunication
{
    public class InfoConnection : ILoggable
    {
        private readonly string serverAddress;
        private readonly Logger logger;
        public InfoConnection(String serverAddress, Logger logger)
        {
            this.serverAddress = serverAddress;
            this.logger = logger;
        }

        public ServerInfo DownloadInfo()
        {
            try { 
                using (TcpClient tcpClient = new TcpClient(serverAddress, TcpConstants.INFO_SERVER_PORT))
                {
                    using(Stream stream = tcpClient.GetStream())
                    {
                        return TextEncoder.ReadJson<ServerInfo>(stream);
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Log(this, "Info downloading wasn't successful");
                throw ex;
            }
        }

        public string GetLogSource()
        {
            return "Server's information downloader";
        }
    }
}
