using Microsoft.AspNetCore.Http;
using Portfolio.Web.Extensions;

namespace Portfolio.Web.Tests.Extensions;

public class RequestExtensionsTests
{
	[Test]
	public void IsAjaxRequest_ReturnsTrue_ForJsonContentType()
	{
		//Arrange
		var context = new DefaultHttpContext();
		context.Request.ContentType = "application/json";

		//Act
		var result = context.Request.IsAjaxRequest();

		//Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsAjaxRequest_ReturnsTrue_ForXmlHttpRequestHeader()
	{
		//Arrange
		var context = new DefaultHttpContext();
		context.Request.Headers.XRequestedWith = "XMLHttpRequest";

		//Act
		var result = context.Request.IsAjaxRequest();

		//Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsAjaxRequest_ReturnsFalse_ForNonAjaxRequest()
	{
		//Arrange
		var context = new DefaultHttpContext();
		context.Request.ContentType = "text/html";

		//Act
		var result = context.Request.IsAjaxRequest();

		//Assert
		Assert.That(result, Is.False);
	}
}