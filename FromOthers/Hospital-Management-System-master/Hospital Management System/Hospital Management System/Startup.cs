using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hospital_Management_System.Startup))]
namespace Hospital_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
