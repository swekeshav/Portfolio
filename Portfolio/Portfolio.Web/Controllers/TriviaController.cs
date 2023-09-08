using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Filters;

namespace Portfolio.Web.Controllers;

[TypeFilter(typeof(ViewExceptionFilter))]
public class TriviaController : ControllerBase
{
	public TriviaController() { }

	public IActionResult ShowTrivia()
	{
		return View();
	}
}
