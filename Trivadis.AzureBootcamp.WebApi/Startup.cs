using Microsoft.Owin;
using Owin;
using Trivadis.AzureBootcamp.WebApi.Authentication;
using Trivadis.AzureBootcamp.WebApi.Common;

[assembly: OwinStartup(typeof(Trivadis.AzureBootcamp.WebApi.Startup))]
namespace Trivadis.AzureBootcamp.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure SignalR
            SignalR.Configure(app);

            // Configure Azure B2C
            // Lab

            // Configure WebApi
            Common.WebApi.Configure(app);
        }
    }
}
