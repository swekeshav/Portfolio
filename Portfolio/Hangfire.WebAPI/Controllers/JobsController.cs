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

	[HttpPost("[action]")]
	public IActionResult Discount()
	{
		const int timeInSeconds = 3;
		var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Welcome to Hangfire Implementation!"),
			TimeSpan.FromSeconds(timeInSeconds));

		return Ok($"Job Id: {jobId}. Discount email will be sent in {timeInSeconds} seconds.");
	}

	[HttpPost("[action]")]
	public IActionResult Schedule()
	{
		const string cronPeriod = "*/10 * * * * *";
		RecurringJob.AddOrUpdate("Schedule Report", () => Console.WriteLine("Reports have been generated."), cronPeriod);

		return Ok("Report generation scheduled.");
	}
}
