using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers
{
    [Authorize, ApiController, Route("user")]
    public class UserController : Controller
    {
        [HttpGet, Route("")]
        public dynamic GetAll()
        {
            var user = User;
            Dictionary<string, string> claims = user.Claims.ToDictionary(c => c.Type, c => c.Value);
            return claims;
        }
    }
}
