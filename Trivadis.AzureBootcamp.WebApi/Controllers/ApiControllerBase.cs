using System.Web.Http;
using System.Web.Http.Cors;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApi.Controllers
{
    /// <summary>
    /// Base class for all API Controllers
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiControllerBase : ApiController
    {
        public ApiControllerBase()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        protected ILogger Logger { get; private set; }
    }
}
