using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Trivadis.AzureBootcamp.CrossCutting
{
    public static class CustomClaimTypes
    {
        public const string Name = "name";

        /// <summary>
        /// http://identity/claims/tokenid
        /// </summary>
        public const string TokenId = "http://identity/claims/tokenid";

        /// <summary>
        /// http://schemas.microsoft.com/identity/claims/objectidentifier
        /// </summary>
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        /// <summary>
        /// http://schemas.microsoft.com/claims/authnclassreference
        /// </summary>
        public const string Acr = "http://schemas.microsoft.com/claims/authnclassreference";
    }

    /// <summary>
    /// Extensions for the claims principal object
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public static string GetToken(this IPrincipal principal)
        {
            return principal.GetClaim(CustomClaimTypes.TokenId).Value;
        }

        public static string GetObjectIdentifier(this IPrincipal principal)
        {
            return principal.GetClaim(CustomClaimTypes.ObjectIdentifier).Value;
        }

        public static string GetAcr(this IPrincipal principal)
        {
            return principal.GetClaim(CustomClaimTypes.Acr).Value;
        }

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
