using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManualOAuth.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        public IActionResult Index()
        {
            Dictionary<string, string> claimTable = new Dictionary<string, string>();

            foreach(var claim in User.Claims)
            {
                claimTable[claim.Type] = claim.Value;
            }

            return View(claimTable);
        }
    }
}
