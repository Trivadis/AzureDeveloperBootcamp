using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
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
                var bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext as System.IdentityModel.Tokens.BootstrapContext;

                viewmodel.Username = User.Identity.Name;
                viewmodel.BearerToken = bootstrapContext.Token;
            }

            return View(viewmodel);
        }

        [HttpGet]
        public void LoginAzureB2C()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties() { RedirectUri = "/" }, AzureB2CSettings.SignInPolicyId);
            }
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<AuthenticationDescription> authTypes = HttpContext.GetOwinContext().Authentication.GetAuthenticationTypes();
                HttpContext.GetOwinContext().Authentication.SignOut(authTypes.Select(t => t.AuthenticationType).ToArray());
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