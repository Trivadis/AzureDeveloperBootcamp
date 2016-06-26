using System.Web.Http;
using Trivadis.AzureBootcamp.WebApi.Hubs;

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


        // Sql Database Lab

        // Sql Database Lab
    }
}
