using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web;

public sealed class APIExceptionHandler : IExceptionHandler
{
	readonly ILogger<APIExceptionHandler> _logger;

	public APIExceptionHandler(ILogger<APIExceptionHandler> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogError(
			exception,
			"Exception occurred: {Message}",
			exception.Message);

		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Title = "Internal Server Error",
			Detail = exception.Message
		};

		httpContext.Response.StatusCode = problemDetails.Status.Value;

		await httpContext.Response
			.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}
