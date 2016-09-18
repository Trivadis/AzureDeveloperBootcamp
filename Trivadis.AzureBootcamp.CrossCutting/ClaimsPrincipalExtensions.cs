using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Trivadis.AzureBootcamp.CrossCutting
{
    public static class CustomClaimTypes
    {
        public const string Name = "name";
    }

    /// <summary>
    /// Extensions for the claims principal object
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public static string GetName(this IPrincipal principal)
        {
            return principal.GetClaim(CustomClaimTypes.Name).Value;
        }

        private static Claim GetClaim(this IPrincipal principal, String claimType)
        {
            ClaimsPrincipal user = principal as ClaimsPrincipal;
            if (user != null)
            {
                var claim = user.FindFirst(claimType);
                if (claim != null)
                {
                    return claim;
                }
            }

            throw new ArgumentException("Unable to read ClaimType '" + claimType + "' from principal!");
        }
    }
}
