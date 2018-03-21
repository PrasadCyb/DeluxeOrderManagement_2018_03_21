using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeluxeOM.WebUI.Startup))]
namespace DeluxeOM.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
