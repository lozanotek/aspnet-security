using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace ManualOAuth.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet, Route("")]
        public IActionResult Index()
        {
            var providers = new[]
            {
                GitHubAuthenticationDefaults.AuthenticationScheme,
                GoogleDefaults.AuthenticationScheme
            };

            return View(providers);
        }

        [HttpPost, Route("")]
        public IActionResult Index(string provider)
        {
            var authProps = new AuthenticationProperties();
            authProps.RedirectUri = "/secure";

            return Challenge(authProps, provider);
        }

        [HttpGet, Route("Signout")]
        public async Task<IActionResult> SignoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }
    }
}
