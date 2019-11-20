using System.Web;
using System.Web.Mvc;
using SimpleAuthentication.Mvc;
using Social.Services;

namespace Social.Controllers
{
    public class SocialIdentityCallbackProvider : IAuthenticationCallbackProvider
    {
        private readonly ILoginService loginService;

        public SocialIdentityCallbackProvider(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public ActionResult Process(HttpContextBase context, AuthenticateCallbackData model)
        {
            var loginResult = loginService.LoginUser(model.AuthenticatedClient.UserInformation);
            return new RedirectResult(loginResult.RedirectUrl, true);
        }

        public ActionResult OnRedirectToAuthenticationProviderError(HttpContextBase context, string errorMessage)
        {
            return new ViewResult
            {
                ViewName = "AuthenticateCallback"
            };
        }
    }
}