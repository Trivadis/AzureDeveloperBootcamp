using System.Web.Mvc;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Controllers
{
    /// <summary>
    /// Base controller for all MVC Controllers
    /// </summary>
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        protected ILogger Logger { get; private set; }
    }
}