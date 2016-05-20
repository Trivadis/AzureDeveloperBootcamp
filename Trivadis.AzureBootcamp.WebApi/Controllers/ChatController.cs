using System.Web.Http;
using Trivadis.AzureBootcamp.WebApi.Hubs;
using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Controllers
{
    [RoutePrefix("api/chat")]
    public class ChatController : ApiControllerBase
    {
        private readonly ChatUserManager _chatmanager;

        public ChatController()
        {
            _chatmanager = new ChatUserManager();
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessage(ChatMessageDTO message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ChatUserContext context = _chatmanager.GetContextByUserId(message.SenderUserId);
            if (context == null)
            {
                return BadRequest(string.Format("user context with id {0} not found", message.SenderUserId));
            }

            Logger.Info("{0}({1}) sends {2}. ImageUrl={3}", context.User.Name, context.User.UserId, message.Message, message.ImageUrl ?? "<NONE>");

            message.SenderUserAvatar = context.User.Avatar;
            ChatHubContext.Current.SendChatMessage(message);

            return Ok();
        }

        [HttpPost]
        [Route("join")]
        public IHttpActionResult JoinChat(JoinChatUserDTO user)
        {
            ChatUser chatuser = _chatmanager.Join(user.Name);
            return Ok(chatuser);
        }

        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(_chatmanager.GetUsers());
        }
    }
}