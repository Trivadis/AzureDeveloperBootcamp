using System.Collections.Generic;
using System.Linq;

namespace Trivadis.AzureBootcamp.WebApi.Models
{
    internal class ChatUserContext
    {
        private readonly HashSet<string> _signalrConnections = new HashSet<string>();
        public ChatUser User { get; set; }

        public void AddConnection(string connectionId)
        {
            _signalrConnections.Add(connectionId);
        }

        public void RemoveConnection(string connectionId)
        {
            _signalrConnections.Remove(connectionId);
        }

        public bool HasConnections { get { return _signalrConnections.Any(); } }
    }
}