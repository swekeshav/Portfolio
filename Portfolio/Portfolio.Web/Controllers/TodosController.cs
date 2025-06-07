using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers;

public class TodosController(ITodosService todosService) : Controller
{
	readonly ITodosService _todosService = todosService;

	[HttpGet]
	public async Task<IActionResult> ShowTodos()
	{
		var todos = await _todosService.GetTodos();
		throw new Exception("This is a test exception to demonstrate error handling in TodosController.");
		return View(todos);
	}

	[HttpPost]
	public async Task<IActionResult> AddTodo([FromForm] TodoInputViewModel newTodo)
	{
		await _todosService.AddTodo(newTodo);
		return RedirectToAction("ShowTodos");
	}
}
