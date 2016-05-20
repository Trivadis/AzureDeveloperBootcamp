using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Hubs
{
    public class ChatHubContext
    {
        private readonly static Lazy<ChatHubContext> _instance = new Lazy<ChatHubContext>(
            () => new ChatHubContext(GlobalHost.ConnectionManager.GetHubContext<ChatHub, IChatHubClient>()));

        private readonly IHubContext<IChatHubClient> _context;
        private ChatHubContext(IHubContext<IChatHubClient> context)
        {
            _context = context;
        }

        public void SendChatMessage(ChatMessageDTO message)
        {
            _context.Clients.All.SendMessageToClient(message);
        }

        public static ChatHubContext Current
        {
            get { return _instance.Value; }
        }
    }
}