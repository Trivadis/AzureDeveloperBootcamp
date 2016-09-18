using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights.Extensibility;
using Owin;
using Trivadis.AzureBootcamp.CrossCutting;

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

            config.Filters.Add(new ApiAuthorizeAttribute());
            config.Filters.Add(new ApiRequestLoggerFilter());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.EnableCors();

            // Provide only JSON Formatter
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Application Insights
            // https://github.com/Azure/azure-content/blob/master/articles/application-insights/app-insights-api-custom-events-metrics.md#developer-mode
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            TelemetryConfiguration.Active.InstrumentationKey = Settings.ApplicationInsightsInstrumentationKey;

            config.MapHttpAttributeRoutes();
        }
    }
}
