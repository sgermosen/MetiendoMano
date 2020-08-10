using WebPage.Services;
using Cloudmersive.APIClient.NETCore.Speech.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace WebPage
{
    public static class Program
    {
        //Free key. Do not use this on production.
        //Visit https://account.cloudmersive.com/default
        const string API_KEY = "83d187fc-077a-4d47-8685-46f4e9c475d1";

        public static int Main(string[] args)
        {
            Configuration.Default.AddApiKey("Apikey", API_KEY);

            var host = CreateHostBuilder(args).Build();
            try
            {
                using (var services = host.Services.CreateScope())
                {
                    var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();
                    if (!dbContext.IsDataFetched())
                    {
                        dbContext.Migrate();
                    }
                }

                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                File.WriteAllText("err_log", ex.Message);
                return -1;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
