using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.API;

[Route("api/todos")]
[ApiController]
public class TodosAPIController : APIControllerBase
{
	readonly ITodosService _todosService;

	public TodosAPIController(ITodosService todosService)
	{
		_todosService = todosService;
	}

	[HttpPatch("status/toggle")]
	public async Task UpdateStatus([FromForm] TodoStatusViewModel todoStatus)
	{
		await _todosService.ToggleStatus(todoStatus);
	}

	[HttpPost]
	public async Task<IActionResult> AddTodo([FromForm] TodoInputViewModel newTodo)
	{
		await _todosService.AddTodo(newTodo);
		return RedirectToAction("ShowTodos", "Todos", new { area = "TaskManager" });
	}
}
