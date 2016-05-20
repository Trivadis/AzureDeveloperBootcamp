using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Trivadis.AzureBootcamp.CrossCutting;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    internal class AzureB2C
    {
        private readonly ILogger _log = LogManager.GetLogger(typeof(AzureB2C));

        public static void Configure(IAppBuilder app)
        {
            new AzureB2C().ConfigureAuthentication(app);
        }

        private void ConfigureAuthentication(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            OpenIdConnectAuthenticationOptions options = new OpenIdConnectAuthenticationOptions
            {
                // These are standard OpenID Connect parameters, with values pulled from web.config
                ClientId = AzureB2CSettings.ClientId,
                RedirectUri = AzureB2CSettings.RedirectUri,
                PostLogoutRedirectUri = AzureB2CSettings.RedirectUri,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = OnAuthenticationFailed,
                    RedirectToIdentityProvider = OnRedirectToIdentityProvider,
                    AuthorizationCodeReceived = OnAuthorizationCodeReceived,
                    MessageReceived = OnMessageReceived
                },

                ResponseType = "code id_token",
                Scope = "openid offline_access",

                // The PolicyConfigurationManager takes care of getting the correct Azure AD authentication
                // endpoints from the OpenID Connect metadata endpoint.  It is included in the PolicyAuthHelpers folder.
                ConfigurationManager = new PolicyConfigurationManager(
                    AzureB2CSettings.MetadataAddress,
                    new string[] { AzureB2CSettings.SignInPolicyId }),

                // This piece is optional - it is used for displaying the user's name in the navigation bar.
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name",
                },
            };

            app.UseOpenIdConnectAuthentication(options);
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

        // This notification can be used to manipulate the OIDC request before it is sent.  Here we use it to send the correct policy.
        private async Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {

            PolicyConfigurationManager mgr = notification.Options.ConfigurationManager as PolicyConfigurationManager;
            if (notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
            {
                OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseRevoke.Properties.Dictionary[AzureB2CSettings.PolicyKey]);
                notification.ProtocolMessage.IssuerAddress = config.EndSessionEndpoint;
            }
            else
            {
                OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseChallenge.Properties.Dictionary[AzureB2CSettings.PolicyKey]);
                notification.ProtocolMessage.IssuerAddress = config.AuthorizationEndpoint;
            }
        }

        private async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification notification)
        {
            _log.Debug("OnAuthorizationCodeReceived: \r\n{0}\r\nCode={1}, \r\nJwtSecurityToken={2}",
                string.Join("\r\n", notification.AuthenticationTicket.Identity.Claims.Select(f => "Claim " + f.Type + "=" + f.Value)),
                notification.Code,
                notification.JwtSecurityToken);

            ClientCredential credential = AzureB2CSettings.CreateClientCredential();
            string mostRecentPolicy = notification.AuthenticationTicket.Identity.FindFirst(CustomClaimTypes.Acr).Value;

            AuthenticationContext authContext = new AuthenticationContext(AzureB2CSettings.Authority);
            AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(notification.Code, new Uri(AzureB2CSettings.RedirectUri), credential, new string[] { AzureB2CSettings.ClientId }, mostRecentPolicy);

            notification.AuthenticationTicket.Identity.AddClaim(new Claim(CustomClaimTypes.TokenId, result.Token));
        }

        // Used for avoiding yellow-screen-of-death
        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            _log.Error("AuthenticationFailed!\r\nError={0}\r\nErrorDescription={1}\r\n{0}",
                notification.ProtocolMessage.Error,
                 notification.ProtocolMessage.ErrorDescription,
                notification.Exception.ToString());

            notification.HandleResponse();
            notification.Response.Redirect("/Home/OpenIdError?message=" + notification.ProtocolMessage.ErrorDescription);
            return Task.FromResult(0);
        }
    }
}