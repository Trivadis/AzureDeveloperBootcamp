using System.Configuration;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    internal static class AzureB2CSettings
    {
        public static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static readonly string RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        public static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        public static readonly string SignInPolicyId = ConfigurationManager.AppSettings["ida:SignInPolicyId"];

    }
}