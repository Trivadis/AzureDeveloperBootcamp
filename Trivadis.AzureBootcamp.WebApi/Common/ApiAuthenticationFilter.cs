using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Trivadis.AzureBootcamp.CrossCutting;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    public class ApiAuthenticationFilter : FilterAttribute, IAuthenticationFilter
    {
        public System.Threading.Tasks.Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            OnAuthentication(context);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="context">The authentication context.</param>
        private void OnAuthentication(HttpAuthenticationContext context)
        {
            if (context.Principal.Identity.IsAuthenticated)
            {
                ClaimsPrincipal user = (ClaimsPrincipal)context.Principal;
                ClaimsIdentity identity = (ClaimsIdentity)user.Identity;
                identity.AddClaim(new Claim(ClaimTypes.Name, user.GetName()));
            }
        }

        public System.Threading.Tasks.Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}