using System.Web.Mvc;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Common
{
    public class MvcGlobalExceptionHandler : IExceptionFilter
    {
        private static readonly ILogger _log = LogManager.GetLogger(typeof(MvcGlobalExceptionHandler));

        public void OnException(ExceptionContext filterContext)
        {
            _log.Error("*** OnException ***\r\n{0}", filterContext.Exception);

            filterContext.ExceptionHandled = true;
            ErrorViewBuilder.UnhandledError(filterContext);
        }
    }
}