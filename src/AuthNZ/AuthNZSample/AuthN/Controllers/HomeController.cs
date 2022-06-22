using AspNet.Security.OAuth.GitHub;
using Auth0.AspNetCore.Authentication;
using AuthN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Okta.AspNetCore;

namespace AuthN.Controllers;

public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        var providers = new[]
        {
            GitHubAuthenticationDefaults.AuthenticationScheme,
            GoogleDefaults.AuthenticationScheme,
            MicrosoftAccountDefaults.AuthenticationScheme,
            OktaDefaults.MvcAuthenticationScheme,
            Auth0Constants.AuthenticationScheme
        };

        return View(new HomeViewModel { Providers = providers });
    }

    [HttpPost]
    public IActionResult Index(string provider)
    {
        var authProps = new AuthenticationProperties
        {
            RedirectUri = "/secure"
        };

        return Challenge(authProps, provider);
    }

    [HttpGet, Route("signout")]
    public async Task<IActionResult> SignoutAsync()
    {
        await HttpContext.SignOutAsync();
        return Redirect("~/");
    }
}
