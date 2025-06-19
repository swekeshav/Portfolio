using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

public class TodosController(ITodosService todosService) : Controller
{
	readonly ITodosService _todosService = todosService;

	[HttpGet]
	public async Task<IActionResult> ShowTodos()
	{
		var todos = await _todosService.GetTodos();
		return View(todos);
	}
}
