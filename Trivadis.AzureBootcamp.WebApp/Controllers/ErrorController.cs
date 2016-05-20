using System;
using System.Globalization;
using System.Web.Mvc;
using Trivadis.AzureBootcamp.WebApp.Common;
using Trivadis.AzureBootcamp.WebApp.ViewModels;

namespace Trivadis.AzureBootcamp.WebApp.Controllers
{
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult UnhandledError()
        {
            Exception error = TempData[ErrorViewBuilder.TempData_Key_Exception] as Exception;
            ErrorViewModel viewmodel = ErrorViewModel.CreateFromError(error, "Unhandled Error Occured");
            return View(viewmodel);
        }

        [HttpGet]
        public ActionResult PageNotFound(String aspxerrorpath)
        {
            ErrorViewModel viewmodel = new ErrorViewModel();
            viewmodel.ErrorText = String.Format(CultureInfo.CurrentCulture, "Page {0} not found", aspxerrorpath);
            return View(viewmodel);
        }
    }
}