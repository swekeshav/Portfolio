using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Portfolio.Web.Extensions;
using Portfolio.Web.Models;
using System.Diagnostics;

namespace Portfolio.Web;

public sealed class RazorExceptionHandler : IExceptionHandler
{
	readonly IRazorViewEngine _viewEngine;
	readonly ITempDataProvider _tempDataProvider;

	public RazorExceptionHandler(
		IRazorViewEngine viewEngine,
		ITempDataProvider tempDataProvider)
	{
		_viewEngine = viewEngine;
		_tempDataProvider = tempDataProvider;
	}

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (httpContext.Request.IsAjaxRequest()) return false;

		httpContext.Response.StatusCode = 500;
		httpContext.Response.ContentType = "text/html";

		var errorViewModel = new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? httpContext.TraceIdentifier
		};

		var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
		{
			Model = errorViewModel
		};

		var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

		var viewResult = _viewEngine.FindView(actionContext, "Error", isMainPage: false);
		if (!viewResult.Success)
		{
			await httpContext.Response.WriteAsync("View 'Error' not found.");
			return true;
		}

		await using var writer = new StringWriter();
		var viewContext = new ViewContext(
			actionContext,
			viewResult.View,
			viewData,
			new TempDataDictionary(httpContext, _tempDataProvider),
			writer,
			new HtmlHelperOptions()
		);

		await viewResult.View.RenderAsync(viewContext);
		var renderedView = writer.ToString();

		await httpContext.Response.WriteAsync(renderedView, cancellationToken);

		return true;
	}
}
