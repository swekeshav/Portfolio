using Portfolio.Web.Models;
using System.Text.Json;

namespace Portfolio.Web.Services;

public class TodosService : ITodosService
{
    static List<TodoViewModel> TodosSet { get; set; } = [];

    public void AddTodo(TodoInputViewModel newTodo)
    {
        TodosSet.Add(new TodoViewModel { Title = newTodo.Title?? "Test Todo" });
        var todosJson = JsonSerializer.Serialize(TodosSet);
        File.WriteAllText("todos.json", todosJson);
    }

    public async Task<FrontPageViewModel> GetTodos()
    {
        var todosJson = await File.ReadAllTextAsync("todos.json");
        var todos = JsonSerializer.Deserialize<List<TodoViewModel>>(todosJson);
        return new FrontPageViewModel
        {
            Todos = todos
        };
    }
}
