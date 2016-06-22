using System.Linq;
using System.Web.Http;
using Trivadis.AzureBootcamp.WebApi.Hubs;
using Trivadis.AzureBootcamp.WebApi.Models;

namespace Trivadis.AzureBootcamp.WebApi.Controllers
{
    // For debugging only!
    [AllowAnonymous]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiControllerBase
    {
        [HttpGet]
        [Route("users")]
        public IHttpActionResult Users()
        {
            return Ok(new ChatUserManager().GetUsers());
        }

        [HttpGet]
        [Route("chatmessages")]
        public IHttpActionResult ChatMessages()
        {
            return Ok(new ChatDbContext().ChatMessages.ToList());
        }
    }
}
