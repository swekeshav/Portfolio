using Microsoft.AspNetCore.Mvc;

namespace Hangfire.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("Hello from hangfire web api!");
	}

	[HttpPost("[action]")]
	public IActionResult Welcome()
	{
		var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Welcome to Hangfire Implementation!"));

		return Ok($"Job Id: {jobId}. Welcome email has been sent.");
	}
}
