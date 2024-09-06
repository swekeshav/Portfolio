using Microsoft.AspNetCore.Mvc;

namespace Hangfire.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("Hello from hangfire web api!");
	}
}
