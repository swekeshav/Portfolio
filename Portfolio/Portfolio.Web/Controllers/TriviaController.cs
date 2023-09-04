using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

public class TriviaController : Controller
{
    private readonly ITriviaService _triviaService;

    public TriviaController(ITriviaService triviaService)
    {
        _triviaService = triviaService;
    }

    public async Task<IActionResult> ShowTrivia()
    {
        var questions = await _triviaService.RetrieveTrivia();
        ViewData["triviaQuestionsList"] = questions;
        return View();
    }
}
