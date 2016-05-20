using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Hubs
{
    public interface IChatHubClient
    {
        void SendMessageToClient(ChatMessageDTO message);
        void UsersChanged();
    }
}