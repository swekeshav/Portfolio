using Microsoft.AspNetCore.Mvc.Rendering;
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

	public TriviaQuestionsViewModel RetrieveTriviaQuestionsViewModel()
	{
		var viewModel = new TriviaQuestionsViewModel
		{
			Categories = ResolveCategories(),
			Difficulties = ResolveDifficulties()
		};
		return viewModel;
	}

	public List<SelectListItem> ResolveCategories()
	{
		return new List<SelectListItem>
		{
			new SelectListItem {Value="any", Text = "Any Category"},
			new SelectListItem {Value="9", Text = "General Knowledge"},
			new SelectListItem {Value="10", Text = "Entertainment: Books"},
			new SelectListItem {Value="11", Text = "Entertainment: Film"},
			new SelectListItem {Value="12", Text = "Entertainment: Music"},
			new SelectListItem {Value="13", Text = "Entertainment: Musicals & Theatres"},
			new SelectListItem {Value="14", Text = "Entertainment: Television"},
			new SelectListItem {Value="15", Text = "Entertainment: Video Games"},
			new SelectListItem {Value="16", Text = "Entertainment: Board Games"},
			new SelectListItem {Value="17", Text = "Science & Nature"},
			new SelectListItem {Value="18", Text = "Science: Computers"},
			new SelectListItem {Value="19", Text = "Science: Mathematics"},
			new SelectListItem {Value="20", Text = "Mythology"},
			new SelectListItem {Value="21", Text = "Sports"},
			new SelectListItem {Value="22", Text = "Geography"},
			new SelectListItem {Value="23", Text = "History"},
			new SelectListItem {Value="24", Text = "Politics"},
			new SelectListItem {Value="25", Text = "Art"},
			new SelectListItem {Value="26", Text = "Celebrities"},
			new SelectListItem {Value="27", Text = "Animals"},
			new SelectListItem {Value="28", Text = "Vehicles"},
			new SelectListItem {Value="29", Text = "Entertainment: Comics"},
			new SelectListItem {Value="30", Text = "Science: Gadgets"},
			new SelectListItem {Value="31", Text = "Entertainment: Japanese Anime & Manga"},
			new SelectListItem {Value="32", Text = "Entertainment: Cartoon & Animations"},
		};
	}

	public List<SelectListItem> ResolveDifficulties()
	{
		return new List<SelectListItem>
		{
			new SelectListItem {Value="any", Text = "Any Difficulty"},
			new SelectListItem {Value="easy", Text = "Easy"},
			new SelectListItem {Value="medium", Text = "Medium"},
			new SelectListItem {Value="hard", Text = "Hard"},
		};
	}
}
