using System;
using System.ComponentModel.DataAnnotations;

namespace Trivadis.AzureBootcamp.WebApi.Models
{
    public class ChatMessageDTO
    {
        public ChatMessageDTO()
        {
            TimestampUtc = DateTime.UtcNow;
        }

        public String Message { get; set; }
        public String ImageUrl { get; set; }

        [Required]
        public String SenderUserId { get; set; }
        public String SenderUserName { get; set; }
        public String SenderUserAvatar { get; set; }

        public DateTime TimestampUtc { get; set; }
    }
}