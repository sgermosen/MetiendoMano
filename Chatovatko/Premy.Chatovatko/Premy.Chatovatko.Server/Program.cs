using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Server.ClientListener;
using Premy.Chatovatko.Server.Logging;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Premy.Chatovatko.Server
{
    class Program : ILoggable
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chatovatko at your service!");
            Logger logger = new Logger(new ConsoleLoggerOutput());
            try
            {
                ServerConfig config = new ServerConfig(logger);
                if(args.Length == 0)
                { 
                    config.LoadConfig();
                }
                else
                {
                    config.LoadConfig(args[0]);
                }

                X509Certificate2 certificate = X509Certificate2Utils.ImportFromPkcs12File(config.CertAddress);

                logger.LoggerOutputs.Add(new DatabaseLoggerOutput(config));
                logger.Log("Program", "Core", "First phase of booting up ended", false);

                InfoService infoService = new InfoService(logger, config, certificate);
                infoService.RunInBackground();

                GodotFountain godotFountain = new GodotFountain(logger, config, certificate);
                godotFountain.Run();
            }
            catch(Exception ex)
            {
                logger.LogException(ex, "Program", "Core", "Program has crashed.");
            }
            finally
            {
                logger.Close();
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }

        public string GetLogSource()
        {
            return "Core";
        }
    }
}
