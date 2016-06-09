using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class PolicyAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        public string Policy { get; set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties(
                        new Dictionary<string, string>
                        {
                            {AzureB2CSettings.PolicyKey, Policy}
                        })
                    {
                        RedirectUri = "/",
                    }
                    , OpenIdConnectAuthenticationDefaults.AuthenticationType);

            base.HandleUnauthorizedRequest(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class SignInPolicyAttribute : PolicyAuthorize
    {
        public SignInPolicyAttribute()
        {
            Policy = AzureB2CSettings.SignInPolicyId;
        }
    }
}