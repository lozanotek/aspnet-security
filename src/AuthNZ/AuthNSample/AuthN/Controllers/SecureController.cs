using AuthN.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthN.Controllers;

[Authorize]
public class SecureController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var authResult = await HttpContext.AuthenticateAsync();
        return View(new SecureViewModel(authResult));
    }
}
