namespace Portfolio.Web.Extensions;

public static class RequestExtensions
{
	public static bool IsAjaxRequest(this HttpRequest request)
	{
		return IsJsonRequest(request)
			|| IsXmlHttpRequest(request)
			|| IsOctetStreamRequest(request);
	}

	static bool IsOctetStreamRequest(HttpRequest request) =>
		request.ContentType?.StartsWith("application/octet-stream", StringComparison.OrdinalIgnoreCase) == true;

	static bool IsXmlHttpRequest(HttpRequest request) =>
		request.Headers.TryGetValue("X-Requested-With", out var value) && value == "XMLHttpRequest";

	static bool IsJsonRequest(HttpRequest request)
	{
		return request.ContentType?.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) == true
			|| request.Headers.Accept.ToString().Contains("application/json", StringComparison.OrdinalIgnoreCase);
	}
}
