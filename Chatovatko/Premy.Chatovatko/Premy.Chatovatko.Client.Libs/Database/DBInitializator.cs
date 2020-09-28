using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Resources;
using Premy.Chatovatko.Libs.Logging;

namespace Premy.Chatovatko.Client.Libs.Database
{
    public class DBInitializator :ILoggable
    {
        private readonly IClientDatabaseConfig config;
        private readonly Logger logger;
        public DBInitializator(IClientDatabaseConfig config, Logger logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public void DBDelete()
        {
            File.Delete(config.DatabaseAddress);
            logger.Log(this, "Database deleted");
        }

        public void DBInit()
        {
            if (File.Exists(config.DatabaseAddress))
            {
                DBDelete();
            }

            using (var context = new Context(config))
            {
                try
                {
                    string createScript = CLUtils.ReadResource("Premy.Chatovatko.Client.Libs.Database.SqlScripts.sqlBuild.sqll");
                    
                    context.Database.ExecuteSqlCommand(createScript);
                }
                catch(Exception ex)
                {
                    DBDelete();
                    logger.Log(this, "Initialization of database failed.");
                    throw ex;
                }
            }
            logger.Log(this, "Database initialized");

        }

        public void DBEnsureCreated()
        {
            if (File.Exists(config.DatabaseAddress))
            {
                logger.Log(this, "Database exists already");
            }
            else
            {
                DBInit();
            }
        }

        public string GetLogSource()
        {
            return "Database initializator";
        }
    }
}
