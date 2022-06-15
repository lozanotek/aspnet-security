using AuthZ;
using Microsoft.AspNetCore.Mvc;

namespace AuthN.API.Controllers;

[ApiController, Route("submit")]
public class SubmitController : Controller
{
    [HttpGet, Route(""), RequirePermission("CanSubmit")]
    public bool Get()
    {
        var user = User;
        var permissions = user.Permissions();

        return permissions.Contains("CanSubmit");
    }
}