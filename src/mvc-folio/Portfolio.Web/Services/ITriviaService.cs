using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITriviaService
{
	Task RetrieveTriviaQuestions(Paginator<TriviaQuestionInfo> paginator, TriviaQuestionsSearchFilter searchFilter);
	TriviaQuestionsViewModel RetrieveTriviaQuestionsViewModel();
}
