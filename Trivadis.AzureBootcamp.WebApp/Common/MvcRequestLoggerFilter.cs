using System;
using System.Web.Mvc;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Common
{
    internal sealed class MvcRequestLoggerFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String request = filterContext.HttpContext.Request.HttpMethod + " -> " + filterContext.HttpContext.Request.Url.ToString();
            bool isAsync = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAsync)
            {
                request = "ASYNC " + request;
            }

            LogManager.Factory.CreateLogger(filterContext.Controller.GetType()).Debug(request);
        }
    }
}