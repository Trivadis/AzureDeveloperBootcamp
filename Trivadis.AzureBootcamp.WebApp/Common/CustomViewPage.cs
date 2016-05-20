using System;
using System.Web.Mvc;
using Trivadis.AzureBootcamp.CrossCutting;

namespace Trivadis.AzureBootcamp.WebApp.Common
{
    public abstract class CustomViewPage<TModel> : WebViewPage<TModel>
    {
        public string WebRoot
        {
            get { return Settings.WebRoot; }
        }

        public string ApiRoot
        {
            get { return Settings.ApiRoot; }
        }

        public string ApiHost
        {
            get { return Settings.ApiHost; }
        }
    }

    public abstract class CustomViewPage : CustomViewPage<dynamic>
    {
    }
}