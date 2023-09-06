using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Filters;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

[TypeFilter(typeof(ViewExceptionFilter))]
public class TriviaController : ControllerBase
{
    private readonly ITriviaService _triviaService;
    private readonly ILogger<TriviaController> _logger;

    public TriviaController(ITriviaService triviaService, ILogger<TriviaController> logger)
    {
        _triviaService = triviaService;
        _logger = logger;
    }

    public async Task<IActionResult> ShowTrivia()
    {
        TriviaQuestionsList? questions = await _triviaService.RetrieveTrivia();
        throw new ArgumentNullException();
        return View(questions);
    }
}
