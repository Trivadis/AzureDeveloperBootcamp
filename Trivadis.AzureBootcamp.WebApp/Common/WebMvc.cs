using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Extensibility;
using Trivadis.AzureBootcamp.CrossCutting;

namespace Trivadis.AzureBootcamp.WebApp.Common
{
    internal static class WebMvc
    {
        public static void Configure()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Application Insights
            // https://github.com/Azure/azure-content/blob/master/articles/application-insights/app-insights-api-custom-events-metrics.md#developer-mode
            TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            TelemetryConfiguration.Active.InstrumentationKey = Settings.ApplicationInsightsInstrumentationKey;

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }

    internal class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcAuthenticationFilter());
            filters.Add(new MvcGlobalExceptionHandler());
            filters.Add(new MvcRequestLoggerFilter());
        }
    }

    internal class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    internal class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/cssTEST").Include(
                    "~/Content/Site.css"
                ));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/angular-material.css",
                "~/Content/Site.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/jquery-{version}.js",
                     "~/Scripts/jquery.signalR-{version}.js",
                     "~/Scripts/angular.js",
                     "~/Scripts/angular-animate.js",
                     "~/Scripts/angular-aria.js",
                     "~/Scripts/angular-material/angular-material.js",
                     "~/Scripts/ng-file-upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app.js"));
        }
    }
}