using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

public class TriviaController : Controller
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
        TriviaQuestionsList? questions = null;
        try
        {
            questions = await _triviaService.RetrieveTrivia();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        return View(questions);
    }
}
