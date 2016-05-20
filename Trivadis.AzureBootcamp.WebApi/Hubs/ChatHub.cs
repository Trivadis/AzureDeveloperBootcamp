using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Trivadis.AzureBootcamp.CrossCutting.Logging;
using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Hubs
{
    /// <summary>
    /// Signalr connection hub
    /// </summary>
    public class ChatHub : Hub<IChatHubClient>
    {
        private readonly ILogger _log = LogManager.GetLogger(typeof(ChatHub));
        private readonly ChatUserManager _manager = new ChatUserManager();

        public override Task OnConnected()
        {
            ChatUserContext context = _manager.AddSignalrConnection(UserId, this.Context.ConnectionId);

            _log.Info("Connect {0}..UserId={1};ConnectionId={2}", context.User.Name, context.User.UserId, this.Context.ConnectionId);

            Clients.Others.UsersChanged();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ChatUserContext context = _manager.RemoveSignalrConnection(UserId, this.Context.ConnectionId);
            if (context != null)
            {
                _log.Info("Disconnect {0}..UserId={1};ConnectionId={2}", context.User.Name, context.User.UserId, this.Context.ConnectionId);
                Clients.Others.UsersChanged();
            }

            return base.OnDisconnected(stopCalled);
        }

        private string UserId
        {
            // see SignalrProxy.ts
            get { return Context.QueryString["UserId"]; }
        }
    }
}