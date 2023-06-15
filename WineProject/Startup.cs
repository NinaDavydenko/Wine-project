using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WineProject.Startup))]
namespace WineProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
