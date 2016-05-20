using System;
using System.Collections.Generic;
using System.Linq;
using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Hubs
{
    internal class ChatUserManager
    {
        private static readonly Dictionary</* UserId */ string, ChatUserContext> _users = new Dictionary<string, ChatUserContext>();

        public ChatUser Join(string username)
        {
            ChatUser user = new ChatUser();
            user.UserId = Guid.NewGuid().ToString();
            user.Name = username;
            user.Avatar = Avatar.GetNextAvatarSvg();

            ChatUserContext context = new ChatUserContext();
            context.User = user;

            _users.Add(user.UserId, context);

            return user;
        }

        public IEnumerable<ChatUser> GetUsers()
        {
            return _users.Values.Select(f => f.User);
        }

        public ChatUserContext AddSignalrConnection(string userId, string connectionId)
        {
            ChatUserContext context = GetContextByUserId(userId);
            if (context == null)
            {
                throw new ArgumentException(string.Format("user context with id {0} not found", userId));
            }

            context.AddConnection(connectionId);
            return context;
        }

        public ChatUserContext RemoveSignalrConnection(string userId, string connectionId)
        {
            ChatUserContext context = GetContextByUserId(userId);
            if (context != null)
            {
                context.RemoveConnection(connectionId);
                if (!context.HasConnections)
                {
                    _users.Remove(userId);
                    return context;
                }
            }

            return null;
        }

        public ChatUserContext GetContextByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            if (_users.ContainsKey(userId))
            {
                return _users[userId];
            }

            return null;
        }
    }
}