using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Premy.Chatovatko.Server.ClientListener
{
    public class GodotFountain
    {
        private readonly int ServerPort = TcpConstants.MAIN_SERVER_PORT;

        private readonly GodotCounter counter;

        private readonly Logger logger;
        private readonly X509Certificate2 serverCert;
        private readonly ServerConfig config;

        public GodotFountain(Logger logger, ServerConfig config, X509Certificate2 serverCert)
        {
            this.counter = new GodotCounter();
            this.logger = logger;
            this.config = config;
            this.serverCert = serverCert;
        }


        public void Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, ServerPort);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => 
                {  
                    new Godot(counter.Created, logger, config, serverCert, counter).Run(client);
                    
                });
            }
        }
    }
}
