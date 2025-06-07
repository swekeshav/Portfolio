using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace Portfolio.Web.Tests.ExceptionHandler;

[TestFixture]
public class APIExceptionHandlerTests
{
	Mock<ILogger<APIExceptionHandler>> _logger = null!;
	APIExceptionHandler _handler = null!;

	[SetUp]
	public void SetUp()
	{
		_logger = new Mock<ILogger<APIExceptionHandler>>();
		_handler = new APIExceptionHandler(_logger.Object);
	}

	[Test]
	public async Task TryHandleAsync_ReturnsTrue_AndWritesProblemDetails_ForException()
	{
		//Arrange
		var context = new DefaultHttpContext();
		var responseStream = new MemoryStream();
		context.Response.Body = responseStream;

		var exception = new Exception("exception error");

		//Act
		var result = await _handler.TryHandleAsync(context, exception, CancellationToken.None);

		//Assert
		responseStream.Seek(0, SeekOrigin.Begin);
		var body = new StreamReader(responseStream).ReadToEnd();

		Assert.That(result, Is.True);
		StringAssert.Contains("Internal Server Error", body);
		StringAssert.Contains("\"status\":500", body);
		Assert.That(context.Response.StatusCode, Is.EqualTo(500));
	}
}