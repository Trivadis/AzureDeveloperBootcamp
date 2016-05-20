using System;
using System.Configuration;
using System.Globalization;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    internal static class AzureB2CSettings
    {
        private const string OIDCMetadataSuffix = "/.well-known/openid-configuration";
        private static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        private static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static readonly string ClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];

        public const string PolicyKey = "b2cpolicy";

        public static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static readonly string RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        // B2C policy identifiers
        public static readonly string SignInPolicyId = ConfigurationManager.AppSettings["ida:SignInPolicyId"];

        public static string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, AzureB2CSettings.AadInstance, AzureB2CSettings.Tenant, string.Empty, string.Empty);
            }
        }

        public static string MetadataAddress
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, AzureB2CSettings.AadInstance, AzureB2CSettings.Tenant, "/v2.0", AzureB2CSettings.OIDCMetadataSuffix);
            }
        }

        public static ClientCredential CreateClientCredential()
        {
            return new ClientCredential(ClientId, ClientSecret);
        }
    }
}