using System.Configuration;

namespace Trivadis.AzureBootcamp.CrossCutting
{
    public static class Settings
    {
        public static string ApplicationInsightsInstrumentationKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationInsightsInstrumentationKey"];
            }
        }

        /// <summary>
        /// Gets the API Host URL. Ex. http://localhost:1124
        /// </summary>
        public static string ApiHost
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiHost"];
            }
        }

        /// <summary>
        /// Gets the base url for the API. Ex. http://localhost:1124/api
        /// </summary>
        public static string ApiRoot
        {
            get
            {
                var apiRoot = ConfigurationManager.AppSettings["ApiRoot"];
                if (!string.IsNullOrWhiteSpace(apiRoot))
                {
                    return apiRoot.Replace("{ApiHost}", ApiHost);
                }

                return apiRoot;
            }
        }

        public static string WebRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["WebRoot"];
            }
        }

        /// <summary>
        /// Connection String for Azure Storage Account
        /// </summary>
        public static string AzureStorageAccountConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["AzureStorageAccount"];
            }
        }
    }
}