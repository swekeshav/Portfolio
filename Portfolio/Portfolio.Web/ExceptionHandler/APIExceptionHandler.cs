using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web;

internal sealed class APIExceptionHandler : IExceptionHandler
{
	readonly ILogger<APIExceptionHandler> _logger;

	public APIExceptionHandler(ILogger<APIExceptionHandler> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is not BadRequestException badRequestException)
		{
			return false;
		}

		_logger.LogError(
			badRequestException,
			"Exception occurred: {Message}",
			badRequestException.Message);

		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status400BadRequest,
			Title = "Bad Request",
			Detail = badRequestException.Message
		};

		httpContext.Response.StatusCode = problemDetails.Status.Value;

		await httpContext.Response
			.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}

internal sealed class BadRequestException : Exception
{
	public BadRequestException(string message)
		: base(message)
	{
	}
	public BadRequestException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	public BadRequestException()
	{
	}
}
