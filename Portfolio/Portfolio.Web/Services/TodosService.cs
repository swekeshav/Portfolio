using Portfolio.Web.Data;
using Portfolio.Web.Models;
using System.Text.Json;

namespace Portfolio.Web.Services;

public class TodosService : ITodosService
{
	readonly IRepository<TodoViewModel> _todoRepository;

	public TodosService(IRepository<TodoViewModel> todoRepository)
	{
		_todoRepository = todoRepository;
	}

	public async Task AddTodo(TodoInputViewModel newTodo)
	{
		var todo = newTodo.ToTodo();
		await _todoRepository.Add(todo);
	}

	static async Task SaveTodos(List<TodoViewModel> todos)
	{
		var todosJson = JsonSerializer.Serialize(todos);
		await File.WriteAllTextAsync("todos.json", todosJson);
	}

	public async Task<FrontPageViewModel> GetTodos()
	{
		return new FrontPageViewModel
		{
			Todos = (await _todoRepository.GetAll()).ToList()
		};
	}

	public async Task ToggleStatus(TodoStatusViewModel todoStatus)
	{
		var todos = (await _todoRepository.GetAll()).ToList();
		var todo = todos.FirstOrDefault(t => t.UUID == todoStatus.UUID);
		if (todo != null)
		{
			todo.IsCompleted = !todo.IsCompleted;
			await SaveTodos(todos);
		}
	}
}
