using System.Web.Mvc;
using Social.Services;

namespace Social.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISocialIdentityProvider identityProvider;
        private readonly ILoginService loginService;

        public LoginController(ISocialIdentityProvider identityProvider, ILoginService loginService)
        {
            this.identityProvider = identityProvider;
            this.loginService = loginService;
        }

        public ActionResult Index()
        {
            var socialList = identityProvider.GetSupportedIdentities();
            return View(socialList);
        }

        public ActionResult Signout()
        {
            var result = loginService.LogoutUser();
            return RedirectPermanent(result.RedirectUrl);
        }
    }
}
