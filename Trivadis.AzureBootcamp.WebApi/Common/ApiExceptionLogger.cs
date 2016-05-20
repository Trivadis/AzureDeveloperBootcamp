using System.Web.Http.ExceptionHandling;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    /// <summary>
    /// Exception Logger für API calls
    /// </summary>
    internal class ApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var ex = context.Exception;
            var controllerContext = context.ExceptionContext.ControllerContext;
            if (controllerContext != null)
            {
                LogManager.Factory.CreateLogger(controllerContext.Controller.GetType()).Error("***** UNHANDLED API CONTROLLER EXCEPTION *****\r\n{0}", ex.ToString());
            }
            else
            {
                LogManager.Factory.CreateLogger(typeof(ApiExceptionLogger)).Error("***** UNHANDLED API EXCEPTION *****\r\n{0}", ex.ToString());
            }
        }
    }
}