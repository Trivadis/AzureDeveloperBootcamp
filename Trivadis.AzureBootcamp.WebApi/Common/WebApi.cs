using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Trivadis.AzureBootcamp.WebApi.Common
{
    internal static class WebApi
    {
        public static void Configure(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            ConfigureApi(configuration);

            app.UseWebApi(configuration);
        }

        private static void ConfigureApi(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Add(typeof(IExceptionLogger), new ApiExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new ApiGlobalExceptionHandler());

            // Lab ----------------------------------------------------------------------------

            // Lab ----------------------------------------------------------------------------

            config.Filters.Add(new ApiRequestLoggerFilter());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnableCors();

            // Provide only JSON Formatter
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();
        }
    }
}
