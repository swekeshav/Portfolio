using Portfolio.Web.Models;
using System.Text.Json;

namespace Portfolio.Web.Data;

public class TodosRepository : IRepository<TodoViewModel>
{
	const string FileName = "todos.json";

	public async Task Add(TodoViewModel item)
	{
		var todos = (await GetAll()).ToList();
		todos.Add(item);
		Save(todos);
	}

	public void Save(IEnumerable<TodoViewModel> items)
	{
		var todosJson = JsonSerializer.Serialize(items);
		File.WriteAllText(FileName, todosJson);
	}

	public async Task<IEnumerable<TodoViewModel>> GetAll()
	{
		if (!File.Exists(FileName))
			return new List<TodoViewModel>();

		var todosJson = await File.ReadAllTextAsync(FileName);
		return JsonSerializer.Deserialize<List<TodoViewModel>>(todosJson) ?? new List<TodoViewModel>();
	}
}
