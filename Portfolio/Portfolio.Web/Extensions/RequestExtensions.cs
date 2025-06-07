namespace Portfolio.Web.Extensions;

public static class RequestExtensions
{
	public static bool IsAjaxRequest(this HttpRequest request)
	{
		return IsJsonRequest(request)
			|| IsXmlHttpRequest(request);
	}

	static bool IsXmlHttpRequest(HttpRequest request) =>
		request.Headers.TryGetValue("X-Requested-With", out var value) && value == "XMLHttpRequest";

	static bool IsJsonRequest(HttpRequest request) =>
		request.ContentType?.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) == true;
}
