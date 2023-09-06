using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Filters;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

[TypeFilter(typeof(ViewExceptionFilter))]
public class TriviaController : ControllerBase
{
    private readonly ITriviaService _triviaService;

    public TriviaController(ITriviaService triviaService)
    {
        _triviaService = triviaService;
    }

    public async Task<IActionResult> ShowTrivia()
    {
        TriviaQuestionsList? questions = await _triviaService.RetrieveTrivia();
        return View(questions);
    }
}
