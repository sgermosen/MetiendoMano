using Audio2TextRecorder.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Audio2TextRecorder.Web
{
    public static class Program
    {

        public static int Main(string[] args)
        {
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
