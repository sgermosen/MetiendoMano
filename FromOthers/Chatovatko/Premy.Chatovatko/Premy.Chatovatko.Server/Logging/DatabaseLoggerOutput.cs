using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Premy.Chatovatko.Server.Logging
{
    public class DatabaseLoggerOutput : ILoggerOutput
    {
        private readonly ServerConfig config;

        public DatabaseLoggerOutput(ServerConfig config)
        {
            this.config = config;
        }

        public void Close()
        {
            
        }

        public string GetName()
        {
            return "DatabaseLoggerOutput";
        }

        public void Log(ILoggerMessage message)
        {
            Task.Run(() =>
            {
                Logs sqlLogMessage = new Logs
                {
                    Error = message.IsError,
                    Message = message.GetMessage(),
                    Source = message.GetSource(),
                    TimeOfCreation = message.GetTimeOfCreation(),
                    Class = message.GetClassName()
                };

                using (Context con = new Context(config))
                {
                    con.Logs.Add(sqlLogMessage);
                    con.SaveChanges();
                }

            });

        }
    }
}
