using Microsoft.AspNetCore.Mvc;

namespace APIKey.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
	public IActionResult Get()
	{
		var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
		return Ok(claims);
	}
}
