using Contactes.Web.Areas.Identity.Data;
using Contactes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Contactes.Web.Areas.Identity.IdentityHostingStartup))]
namespace Contactes.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContex>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DataContexConnection")));

                services.AddDefaultIdentity<AppUser>()
                    .AddEntityFrameworkStores<DataContex>();
            });
        }
    }
}