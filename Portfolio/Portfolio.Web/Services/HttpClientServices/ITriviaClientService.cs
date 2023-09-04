using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITriviaClientService
{
    Task<TriviaQuestionsList?> FetchRandomTrivia();
}
