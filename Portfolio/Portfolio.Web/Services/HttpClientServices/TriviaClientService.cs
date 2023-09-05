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

    public async Task<TriviaQuestionsList?> FetchRandomTrivia()
    {
        const string queryString = "amount=10";
        HttpRequestMessage requestMessage = new(HttpMethod.Get, _httpClient.BaseAddress + queryString);

        var response = await _httpClient.SendAsync(requestMessage);
        var responseBody = await response.Content.ReadAsStringAsync();

        var deserializedData = JsonSerializer.Deserialize<TriviaQuestionsList>(responseBody,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        foreach (var data in deserializedData!.Results)
        {
            data.Question = HttpUtility.HtmlDecode(data.Question);
            data.Difficulty = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(data.Difficulty.ToLower());
        }
        return deserializedData;
    }
}
