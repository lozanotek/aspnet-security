using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
