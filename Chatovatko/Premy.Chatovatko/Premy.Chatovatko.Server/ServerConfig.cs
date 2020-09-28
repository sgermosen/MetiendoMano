using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Server.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Premy.Chatovatko.Server
{
    public class ServerConfig : ILoggable
    {
        public ServerConfig(Logger logger)
        {
            this.logger = logger;
            Password = null;
        }

        private Logger logger;
        private String connectionString = "";
        private String certPasswd = null;
        private String certAddress = "";

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public string CertPasswd { get => certPasswd; set => certPasswd = value; }
        public string CertAddress { get => certAddress; set => certAddress = value; }
        public string ServerName { get; set; }
        public string Password { get; set; }

        public void LoadConfig(String path = "./config.txt")
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream) {
                    String line = sr.ReadLine();
                    String[] parts = line.Split('=');
                    String name = parts[0];
                    String rest = "";
                    for (int i = 1; i != parts.Length; i++)
                    { 
                        rest += parts[i];
                        if(i != parts.Length - 1)
                        {
                            rest += '=';
                        }
                    }

                    switch (parts[0])
                    {
                        case "ConnectionString":
                            connectionString = rest;
                            break;
                        case "CertPasswd":
                            certPasswd = rest;
                            break;
                        case "CertAddress":
                            certAddress = rest;
                            break;
                        case "ServerName":
                            ServerName = rest;
                            break;
                        case "Password":
                            Password = rest;
                            break;
                        default:
                            logger.Log(this, String.Format("The parameter {0} doesn't exist.", name));
                            break;
                    }
                }
            }
        }

        public string GetLogSource()
        {
            return "Configuration";
        }
    }
}
