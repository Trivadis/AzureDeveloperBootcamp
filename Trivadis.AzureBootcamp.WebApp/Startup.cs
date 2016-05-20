using Microsoft.Owin;
using Owin;
using Trivadis.AzureBootcamp.WebApp.Authentication;
using Trivadis.AzureBootcamp.WebApp.Common;

[assembly: OwinStartup(typeof(Trivadis.AzureBootcamp.WebApp.Startup))]

namespace Trivadis.AzureBootcamp.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure MVC
            WebMvc.Configure();

            // Configure Azure B2C
            AzureB2C.Configure(app);
        }
    }
}
