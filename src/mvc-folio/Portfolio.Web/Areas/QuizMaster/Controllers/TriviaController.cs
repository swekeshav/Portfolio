using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Filters;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

[Area("QuizMaster")]
[TypeFilter(typeof(ViewExceptionFilter))]
public class TriviaController : ControllerBase
{
	private readonly ITriviaService _triviaService;

	public TriviaController(ITriviaService triviaService)
	{
		_triviaService = triviaService;
	}

	public IActionResult ShowTrivia()
	{
		var vm = _triviaService.RetrieveTriviaQuestionsViewModel();
		return View(vm);
	}
}
