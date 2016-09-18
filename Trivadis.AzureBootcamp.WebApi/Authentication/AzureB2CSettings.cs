using System;
using System.Configuration;

namespace Trivadis.AzureBootcamp.WebApi.Authentication
{
    internal static class AzureB2CSettings
    {
        public static readonly string CommonPolicy = ConfigurationManager.AppSettings["ida:PolicyId"];
        public static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        public static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        public static string ClientId
        {
            get
            {
                return ConfigurationManager.AppSettings["ida:ClientId"]; ;
            }
        }
    }
}