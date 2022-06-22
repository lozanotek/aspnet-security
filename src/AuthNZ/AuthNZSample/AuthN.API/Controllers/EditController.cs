using AuthZ;
using Microsoft.AspNetCore.Mvc;

namespace AuthN.API.Controllers;

[ApiController, Route("edit")]
public class EditController : Controller
{
    [HttpGet, Route(""), RequirePermission("CanEdit")]
    public bool Get()
    {
        var user = User;
        var permissions = user.Permissions();

        return permissions.Contains("CanEdit");
    }
}