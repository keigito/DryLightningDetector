using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DryLightning.Startup))]
namespace DryLightning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
