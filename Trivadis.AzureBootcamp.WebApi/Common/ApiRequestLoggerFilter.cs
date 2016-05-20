using System;
using System.Web.Http.Filters;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    internal sealed class ApiRequestLoggerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            String request = actionContext.Request.Method + " -> " + actionContext.Request.RequestUri;
            LogManager.Factory.CreateLogger(actionContext.ControllerContext.Controller.GetType()).Info("API {0}", request);
        }
    }
}