
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Trivadis.AzureBootcamp.WebApi.Authentication
{
    internal class AzureB2C
    {
        public static void Configure(IAppBuilder app)
        {
            new AzureB2C().ConfigureOAuth(app);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(CreateBearerOptionsFromPolicy(AzureB2CSettings.CommonPolicy));
        }

        private OAuthBearerAuthenticationOptions CreateBearerOptionsFromPolicy(string policy)
        {
            var metadataEndpoint = string.Format(AzureB2CSettings.AadInstance, AzureB2CSettings.Tenant, policy);

            TokenValidationParameters tvps = new TokenValidationParameters
            {
                // This is where you specify that your API only accepts tokens from its own clients
                ValidAudience = AzureB2CSettings.ClientId,
                AuthenticationType = policy,
                NameClaimType = "name" //  http://schemas.microsoft.com/identity/claims/objectidentifier"
            };

            return new OAuthBearerAuthenticationOptions
            {
                // This SecurityTokenProvider fetches the Azure AD B2C metadata & signing keys from the OpenIDConnect metadata endpoint
                AccessTokenFormat = new JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider(metadataEndpoint))
            };
        }
    }
}