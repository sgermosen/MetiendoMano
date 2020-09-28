using FrontEnd.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FrontEnd.Startup))]
namespace FrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            ParamaterConfig.Initialize();
            EnumConfig.Start();
            MenuConfig.Initialize();
        }
    }
}
