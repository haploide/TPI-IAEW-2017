using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TuriCorWeb.Startup))]
namespace TuriCorWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
