using AuthZ;
using Microsoft.AspNetCore.Mvc;

namespace AuthN.API.Controllers;

[ApiController, Route("read")]
public class ReadController : Controller
{
    [HttpGet, Route(""), RequirePermission("CanRead")]
    public bool Get()
    {
        var user = User;
        var permissions = user.Permissions();

        return permissions.Contains("CanRead");
    }
}