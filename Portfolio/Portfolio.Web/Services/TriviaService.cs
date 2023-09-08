using Portfolio.Web.Models;
using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;

namespace Portfolio.Web.Services;

public class TriviaService : ITriviaService
{
	private readonly ITriviaClientService _clientService;

	public TriviaService(ITriviaClientService clientService)
	{
		_clientService = clientService;
	}

	public async Task RetrieveTriviaQuestions(Paginator<TriviaQuestionInfo> paginator, TriviaQuestionsSearchFilter searchFilter)
	{
		searchFilter.QuestionCount = paginator.PageSize;
		IEnumerable<TriviaQuestionInfo> questions = (await _clientService.FetchTrivia(searchFilter)).Results;

		if (!string.IsNullOrWhiteSpace(paginator.SortColumn))
		{
			questions = questions.AsQueryable().OrderBy(paginator.SortColumn + " " + paginator.SortDirection).ToList();
		}

		paginator.Data = new ReadOnlyCollection<TriviaQuestionInfo>(questions.ToList());
	}
}
