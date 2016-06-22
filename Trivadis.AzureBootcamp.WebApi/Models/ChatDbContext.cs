using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Trivadis.AzureBootcamp.WebApi.Models
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext() : base("name=ChatDbContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChatDbContext>());
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }


    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String SenderUserId { get; set; }

        [Required]
        public String SenderUserName { get; set; }

        public String Message { get; set; }
        public String ImageUrl { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public static ChatMessage FromDto(ChatMessageDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ChatMessage entity = new ChatMessage();
            entity.ImageUrl = dto.ImageUrl;
            entity.Message = dto.Message;
            entity.SenderUserId = dto.SenderUserId;
            entity.SenderUserName = dto.SenderUserName;
            entity.Created = DateTimeOffset.Now;
            return entity;
        }
    }
}