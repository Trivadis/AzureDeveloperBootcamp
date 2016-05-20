using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    internal static class SignalR
    {
        public static void Configure(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJavaScriptProxies = false
                };

                map.RunSignalR(hubConfiguration);
            });
        }
    }
}