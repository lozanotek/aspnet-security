using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApiAuth.Controllers
{
    [Authorize, ApiController, Route("user")]
    public class UserController : Controller
    {
        [HttpGet, Route("")]
        public dynamic GetAll()
        {
            var user = User;
            var claims = user.Claims.ToDictionary(c => c.Type, c => c.Value);

            return claims;
        }
    }
}
