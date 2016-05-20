using System;
using System.Configuration;

namespace Trivadis.AzureBootcamp.WebApi.Authentication
{
    internal static class AzureB2CSettings
    {
        private static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        private static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static readonly string CommonPolicy = ConfigurationManager.AppSettings["ida:PolicyId"];

        private const string MetadataSuffix = ".well-known/openid-configuration";

        public static string MetadataAddress
        {
            get
            {
                return String.Format(AadInstance, Tenant, "v2.0", MetadataSuffix, CommonPolicy);
            }
        }

        public static string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings["ida:ClientId"]; ;
            }
        }
    }
}