using System;
using System.ComponentModel.DataAnnotations;

namespace Trivadis.AzureBootcamp.WebApi.Models
{
    public class ChatMessageDTO
    {
        public ChatMessageDTO()
        {
            Timestamp = DateTimeOffset.Now;
        }

        public String Message { get; set; }
        public String ImageUrl { get; set; }

        [Required]
        public String SenderUserId { get; set; }
        public String SenderUserName { get; set; }
        public String SenderUserAvatar { get; set; }

        public bool IsUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Message))
                    return false;

                Uri tmp;
                if (!Uri.TryCreate(Message, UriKind.Absolute, out tmp))
                    return false;

                return tmp.Scheme == "http" || tmp.Scheme == "https";
            }
        }

        public DateTimeOffset Timestamp { get; set; }
    }
}