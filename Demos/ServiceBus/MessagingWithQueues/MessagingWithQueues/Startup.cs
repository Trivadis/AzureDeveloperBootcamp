using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MessagingWithQueues.Startup))]
namespace MessagingWithQueues
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
