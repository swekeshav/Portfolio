using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITriviaService
{
    Task<TriviaQuestionsList?> RetrieveTrivia();
}
