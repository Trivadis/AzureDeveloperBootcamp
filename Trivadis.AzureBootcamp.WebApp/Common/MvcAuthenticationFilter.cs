using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Trivadis.AzureBootcamp.CrossCutting;

namespace Trivadis.AzureBootcamp.WebApp.Common
{
    public class MvcAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.Principal.Identity.IsAuthenticated)
            {
                ClaimsPrincipal user = (ClaimsPrincipal)filterContext.Principal;
                ClaimsIdentity identity = (ClaimsIdentity)user.Identity;
                identity.AddClaim(new Claim(ClaimTypes.Name, user.GetName()));
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }
    }
}