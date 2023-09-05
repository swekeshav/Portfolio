using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public class TriviaService : ITriviaService
{
    private readonly ITriviaClientService _clientService;

    public TriviaService(ITriviaClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<TriviaQuestionsList?> RetrieveTrivia()
    {
        return await _clientService.FetchRandomTrivia();
    }
}
