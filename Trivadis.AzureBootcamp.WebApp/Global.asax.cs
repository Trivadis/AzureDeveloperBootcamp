using System;
using System.Web;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp
{
    /// <summary>
    /// Global.asax is for IIS Event Logging only! Configuration of Mvc is done through in Startup.cs
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILogger _log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start(object sender, EventArgs e)
        {
            _log.Info("*** {0} Start  ***", typeof(MvcApplication).Namespace);
        }

#if DEBUG
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // IIS Request 

            _log.Debug("{0} -> {1}", Request.HttpMethod, Request.Url.AbsoluteUri);
        }
#endif

        protected void Application_End(object sender, EventArgs e)
        {
            _log.Info("*** {0} End  ***", typeof(MvcApplication).Namespace);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContext httpContext = ((HttpApplication)sender).Context;
            Exception ex = Server.GetLastError();
            _log.Error("*** Unhandled Application Error ***\r\n{0}", ex.ToString());
        }
    }
}