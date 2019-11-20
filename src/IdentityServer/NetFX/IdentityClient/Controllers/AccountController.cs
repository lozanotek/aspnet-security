using System.Web;
using System.Web.Mvc;

namespace IdentityClient.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult SignIn()
        {
            return Redirect("~/");
        }

        public ActionResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("~/");
        }
    }
}