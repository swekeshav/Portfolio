using Microsoft.AspNetCore.Authentication;
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
	Mock<IExceptionPolicy> _exceptionPolicy = null!;
	RazorExceptionHandler _handler = null!;

	[SetUp]
	public void SetUp()
	{
		_viewEngine = new Mock<IRazorViewEngine>();
		_tempDataProvider = new Mock<ITempDataProvider>();
		_exceptionPolicy = new Mock<IExceptionPolicy>();
		_handler = new RazorExceptionHandler(_viewEngine.Object, _tempDataProvider.Object, _exceptionPolicy.Object);
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

		_exceptionPolicy.Setup(x => x.ShouldLogout(It.IsAny<Exception>())).Returns(false);

		_viewEngine.Setup(v => v.FindView(It.IsAny<Microsoft.AspNetCore.Mvc.ActionContext>(), "Error", false))
			.Returns(ViewEngineResult.NotFound("Error", new[] { "Error" }));

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

		_exceptionPolicy.Setup(x => x.ShouldLogout(It.IsAny<Exception>())).Returns(false);

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

	[Test]
	public async Task TryHandleAsync_ShouldLogout_TriggersSignOutAndRedirect()
	{
		//Arrange
		var context = new DefaultHttpContext();
		context.Response.Body = new MemoryStream();

		_exceptionPolicy.Setup(x => x.ShouldLogout(It.IsAny<Exception>())).Returns(true);

		var authService = new Mock<IAuthenticationService>();
		authService.Setup(a => a.SignOutAsync(context, null, null)).Returns(Task.CompletedTask);
		context.RequestServices = new Mock<IServiceProvider>()
		.SetupService(typeof(IAuthenticationService), authService.Object);

		//Act
		var result = await _handler.TryHandleAsync(context, new Exception(), CancellationToken.None);

		Assert.Multiple(() =>
		{
			Assert.That(context.Response.StatusCode, Is.EqualTo(301));
			Assert.That(context.Response.Headers.Location, Is.EqualTo("/Account/Login"));
		});
		Assert.That(result, Is.True);
	}
}

// Helper extension for IServiceProvider mocking
public static class ServiceProviderMockExtensions
{
	public static IServiceProvider SetupService(this Mock<IServiceProvider> sp, Type serviceType, object instance)
	{
		sp.Setup(x => x.GetService(serviceType)).Returns(instance);
		return sp.Object;
	}
}