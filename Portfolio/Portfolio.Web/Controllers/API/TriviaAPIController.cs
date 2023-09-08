using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Helpers;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.API;

[Route("api/trivia")]
[ApiController]
public class TriviaAPIController : APIControllerBase
{
	private readonly ITriviaService _triviaService;

	public TriviaAPIController(ITriviaService triviaService)
	{
		_triviaService = triviaService;
	}

	[HttpGet("questions")]
	public async Task<IActionResult> GetTriviaQuestions([FromQuery] TriviaQuestionsSearchFilter parameters)
	{
		Paginator<TriviaQuestionInfo> paginator = PaginationHelper.BuildPaginator<TriviaQuestionInfo>(Request);
		await _triviaService.RetrieveTriviaQuestions(paginator, parameters);

		return Json(new
		{
			draw = Request.Query["draw"].FirstOrDefault(),
			recordsFiltered = paginator.Data!.Count,
			recordsTotal = paginator.Data!.Count,
			data = paginator.Data
		});
	}
}
