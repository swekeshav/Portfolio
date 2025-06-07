using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Portfolio.Web.Tests.ExceptionHandler;

[TestFixture]
public class RazorExceptionHandlerTests
{
	Mock<IRazorViewEngine> _viewEngine = null!;
	Mock<ITempDataProvider> _tempDataProvider = null!;
	RazorExceptionHandler _handler = null!;

	[SetUp]
	public void SetUp()
	{
		_viewEngine = new Mock<IRazorViewEngine>();
		_tempDataProvider = new Mock<ITempDataProvider>();
		_handler = new RazorExceptionHandler(_viewEngine.Object, _tempDataProvider.Object);
	}

	[Test]
	public async Task TryHandleAsync_ReturnsFalse_ForAjaxRequest()
	{
		//Arrange
		var context = new DefaultHttpContext();
		context.Request.Headers.XRequestedWith = "XMLHttpRequest";

		//Act
		var result = await _handler.TryHandleAsync(context, new Exception(), CancellationToken.None);

		//Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public async Task TryHandleAsync_WritesFallback_WhenViewNotFound()
	{
		//Arrange
		var context = new DefaultHttpContext();
		var responseStream = new MemoryStream();
		context.Response.Body = responseStream;

		_viewEngine.Setup(v => v.FindView(It.IsAny<Microsoft.AspNetCore.Mvc.ActionContext>(), "Error", false))
			.Returns(ViewEngineResult.NotFound("Error", ["Error"]));

		//Act
		var result = await _handler.TryHandleAsync(context, new Exception(), CancellationToken.None);

		//Assert
		responseStream.Seek(0, SeekOrigin.Begin);
		var body = new StreamReader(responseStream).ReadToEnd();

		Assert.That(result, Is.True);
		StringAssert.Contains("View 'Error' not found.", body);
	}

	[Test]
	public async Task TryHandleAsync_RendersErrorView_WhenViewFound()
	{
		//Arrange
		var context = new DefaultHttpContext();
		var responseStream = new MemoryStream();
		context.Response.Body = responseStream;

		var viewMock = new Mock<IView>();
		viewMock.SetupGet(v => v.Path).Returns("Error");
		viewMock.Setup(v => v.RenderAsync(It.IsAny<ViewContext>()))
			.Returns<ViewContext>(vc =>
			{
				vc.Writer.Write("Rendered Error View");
				return Task.CompletedTask;
			});

		_viewEngine.Setup(v => v.FindView(It.IsAny<Microsoft.AspNetCore.Mvc.ActionContext>(), "Error", false))
			.Returns(ViewEngineResult.Found("Error", viewMock.Object));

		//Act
		var result = await _handler.TryHandleAsync(context, new Exception(), CancellationToken.None);

		//Assert
		responseStream.Seek(0, SeekOrigin.Begin);
		var body = new StreamReader(responseStream).ReadToEnd();

		Assert.That(result, Is.True);
		StringAssert.Contains("Rendered Error View", body);
	}
}