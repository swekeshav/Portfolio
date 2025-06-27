using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Portfolio.Web.Filters;

public class ViewExceptionFilter : IExceptionFilter
{
	private readonly IHostEnvironment _hostEnvironment;
	private readonly ILogger<ViewExceptionFilter> _logger;

	public ViewExceptionFilter(IHostEnvironment hostEnvironment, ILogger<ViewExceptionFilter> logger)
	{
		_hostEnvironment = hostEnvironment;
		_logger = logger;
	}

	public void OnException(ExceptionContext context)
	{
		if (!_hostEnvironment.IsDevelopment())
		{
			_logger.LogError(context.Exception.Message);
			return;
		}
		context.Result = new ContentResult
		{
			Content = context.Exception.ToString()
		};
	}
}
