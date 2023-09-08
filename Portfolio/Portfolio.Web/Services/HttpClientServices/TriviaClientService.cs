using Portfolio.Web.Models;
using System.Globalization;
using System.Text.Json;
using System.Web;

namespace Portfolio.Web.Services;

public class TriviaClientService : ITriviaClientService
{
	private readonly HttpClient _httpClient;

	public TriviaClientService(IHttpClientFactory httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient("opentdb");
	}

	public async Task<TriviaQuestions> FetchTrivia(TriviaQuestionsSearchFilter searchFilter)
	{
		string queryString = BuildQueryString(searchFilter);
		HttpRequestMessage requestMessage = new(HttpMethod.Get, _httpClient.BaseAddress + queryString);

		var response = await _httpClient.SendAsync(requestMessage);
		var responseBody = await response.Content.ReadAsStringAsync();

		var deserializedData = JsonSerializer.Deserialize<TriviaQuestions>(responseBody,
			new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

		foreach (var data in deserializedData!.Results)
		{
			data.Question = HttpUtility.HtmlDecode(data.Question);
			data.Difficulty = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Difficulty.ToLower());
		}
		return deserializedData;

		static string BuildQueryString(TriviaQuestionsSearchFilter searchFilter)
		{
			string queryString = $"amount=30";

			if (!searchFilter.Category.Equals("any", StringComparison.OrdinalIgnoreCase))
			{
				queryString += $"&category={searchFilter.Category}";
			}

			if (!searchFilter.Difficulty.Equals("any", StringComparison.OrdinalIgnoreCase))
			{
				queryString += $"&difficulty={searchFilter.Difficulty}";
			}

			return queryString;
		}
	}
}
