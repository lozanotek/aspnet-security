using Microsoft.AspNetCore.Mvc;

namespace AuthN.API.Controllers;

[ApiController, Route("echo")]
public class EchoController : Controller
{
    [HttpGet, Route("")]
    public ClaimEcho[] Get()
    {
        var user = User;
        var claims = user.Claims.Select(c => 
            new ClaimEcho {Type = c.Type, Value = c.Value}).ToArray();

        return claims;
    }
}