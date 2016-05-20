using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(bootcampmobileappService.Startup))]

namespace bootcampmobileappService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}