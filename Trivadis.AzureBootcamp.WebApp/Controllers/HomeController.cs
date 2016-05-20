using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Trivadis.AzureBootcamp.CrossCutting;
using Trivadis.AzureBootcamp.WebApp.Authentication;
using Trivadis.AzureBootcamp.WebApp.ViewModels;

namespace Trivadis.AzureBootcamp.WebApp.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            HomeViewModel viewmodel = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                viewmodel.Username = User.GetName();
                viewmodel.BearerToken = User.GetToken();
            }

            return View(viewmodel);
        }

        [HttpGet]
        [SignInPolicy]
        public ActionResult LoginAzureB2C()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(
                new AuthenticationProperties(
                    new Dictionary<string, string>  {
                        {AzureB2CSettings.PolicyKey,  User.GetAcr() }
                    }), 
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TestError()
        {
            throw new Exception("Hello MVC Exception");
        }

        /// <summary>
        /// Gets called by azure ad b2c
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OpenIdError(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}