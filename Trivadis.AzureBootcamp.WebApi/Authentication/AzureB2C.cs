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
            new AzureB2C().ConfigureAuthentication(app);
        }

        private void ConfigureAuthentication(IAppBuilder app)
        {
            TokenValidationParameters validationParams = new TokenValidationParameters
            {
                ValidAudience = AzureB2CSettings.ClientId,
            };

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenFormat = new JwtFormat(validationParams, new OpenIdConnectCachingSecurityTokenProvider(AzureB2CSettings.MetadataAddress))
            });
        }
    }
}