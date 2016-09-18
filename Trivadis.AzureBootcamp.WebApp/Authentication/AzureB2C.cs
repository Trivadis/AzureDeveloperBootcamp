using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    internal class AzureB2C
    {
        private readonly ILogger _log = LogManager.GetLogger(typeof(AzureB2C));

        public static void Configure(IAppBuilder app)
        {
            new AzureB2C().ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(CreateOptionsFromPolicy(AzureB2CSettings.SignInPolicyId));
        }

        private OpenIdConnectAuthenticationOptions CreateOptionsFromPolicy(string policy)
        {
            return new OpenIdConnectAuthenticationOptions
            {
                // For each policy, give OWIN the policy-specific metadata address, and
                // set the authentication type to the id of the policy
                MetadataAddress = String.Format(AzureB2CSettings.AadInstance, AzureB2CSettings.Tenant, policy),
                AuthenticationType = policy,

                // These are standard OpenID Connect parameters, with values pulled from web.config
                ClientId = AzureB2CSettings.ClientId,
                RedirectUri = AzureB2CSettings.RedirectUri,
                PostLogoutRedirectUri = AzureB2CSettings.RedirectUri,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = AuthenticationFailed,
                    MessageReceived = OnMessageReceived,
                    AuthorizationCodeReceived = OnAuthorizationCodeReceived
                },

                Scope = "openid",
                ResponseType = "id_token",

                // This piece is optional - it is used for displaying the user's name in the navigation bar.
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    SaveSigninToken = true //important to save the token in boostrapcontext
                },
            };
        }

        private async Task OnMessageReceived(MessageReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            _log.Debug("OnMessageReceived: \r\nRequestType={0}\r\nAuthenticationType={1}\r\nRedirectUri={2}\r\nCode={3}",
                notification.ProtocolMessage.RequestType,
                notification.Options.AuthenticationType,
                notification.Options.RedirectUri,
                notification.ProtocolMessage.Code);

            await Task.FromResult(0);
        }

        private Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification notification)
        {
            _log.Debug("OnAuthorizationCodeReceived: \r\n{0}\r\nCode={1}, \r\nJwtSecurityToken={2}",
                string.Join("\r\n", notification.AuthenticationTicket.Identity.Claims.Select(f => "Claim " + f.Type + "=" + f.Value)),
                notification.Code,
                notification.JwtSecurityToken);

            return Task.FromResult(0);
        }

        // Used for avoiding yellow-screen-of-death
        private Task AuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            _log.Error("AuthenticationFailed!\r\nError={0}\r\nErrorDescription={1}\r\n{0}",
               notification.ProtocolMessage.Error,
                notification.ProtocolMessage.ErrorDescription,
               notification.Exception.ToString());

            notification.HandleResponse();
            if (notification.Exception.Message == "access_denied")
            {
                notification.Response.Redirect("/");
            }
            else
            {
                notification.Response.Redirect("/Home/OpenIdError?message=" + notification.ProtocolMessage.ErrorDescription);
            }

            return Task.FromResult(0);
        }
    }
}