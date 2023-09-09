using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Portfolio.Web.Filters;

public class PaginationExceptionFilter : IExceptionFilter
{
	private readonly IHostEnvironment _hostEnvironment;
	private readonly ILogger<ViewExceptionFilter> _logger;

	public PaginationExceptionFilter(IHostEnvironment hostEnvironment, ILogger<ViewExceptionFilter> logger)
	{
		_hostEnvironment = hostEnvironment;
		_logger = logger;
	}

	public void OnException(ExceptionContext context)
	{
		_logger.LogError(context.Exception.Message);
		context.ExceptionHandled = true;
		context.Result = new JsonResult(new { hasFailed = true, data = Array.Empty<object>() });
	}
}
