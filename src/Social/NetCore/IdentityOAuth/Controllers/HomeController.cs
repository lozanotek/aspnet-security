using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace IdentityOAuth.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            var claimTable = new Dictionary<string, string>();

            foreach (var claim in User.Claims)
            {
                claimTable[claim.Type] = claim.Value;
            }

            return View("Claims", claimTable);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
