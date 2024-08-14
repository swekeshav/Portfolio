using Portfolio.Web.Models;
using System.Text.Json;

namespace Portfolio.Web.Services;

public class TodosService : ITodosService
{
    public async Task AddTodo(TodoInputViewModel newTodo)
    {
        List<TodoViewModel> todos = await LoadTodos();
        todos.Add(new TodoViewModel { Title = newTodo.Title ?? "Test Todo" });
        
        SaveTodos(todos);
    }

    private static void SaveTodos(List<TodoViewModel> todos)
    {
        var todosJson = JsonSerializer.Serialize(todos);
        File.WriteAllText("todos.json", todosJson);
    }

    public async Task<FrontPageViewModel> GetTodos()
    {
        List<TodoViewModel> todos = await LoadTodos();
        return new FrontPageViewModel
        {
            Todos = todos
        };
    }

    static async Task<List<TodoViewModel>> LoadTodos()
    {
        var todosJson = await File.ReadAllTextAsync("todos.json");
        List<TodoViewModel> todos;
        try
        {
            todos = JsonSerializer.Deserialize<List<TodoViewModel>>(todosJson) ?? [];
        }
        catch (Exception ex)
        {
            todos = [];
        }
        return todos;
    }
}
