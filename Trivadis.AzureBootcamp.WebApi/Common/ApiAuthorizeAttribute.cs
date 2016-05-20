using System;
using System.Net.Http;
using System.Web.Http;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal sealed class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("Authorization has been denied for this request. " + actionContext.Request.RequestUri)
            };
        }
    }
}