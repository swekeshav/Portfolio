using Portfolio.Web.Models;
using System.Text.Json;

namespace Portfolio.Web.Services;

public class TodosService : ITodosService
{
    public async Task AddTodo(TodoInputViewModel newTodo)
    {
        List<TodoViewModel> todos = await LoadTodos();
        todos.Add(new TodoViewModel { Title = newTodo.Title ?? "Test Todo", UUID = Guid.NewGuid()});
        
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
        string todosJson = "";
        List<TodoViewModel> todos;
        try
        {
            if (File.Exists("todos.json"))
            {
                todosJson = await File.ReadAllTextAsync("todos.json");
            }
            todos = JsonSerializer.Deserialize<List<TodoViewModel>>(todosJson) ?? [];
        }
        catch (Exception ex)
        {
            todos = [];
        }
        return todos;
    }

    public async Task ToggleStatus(TodoStatusViewModel todoStatus)
    {
        var todos = await LoadTodos();
        var todo = todos.FirstOrDefault(t => t.UUID == todoStatus.UUID);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
            SaveTodos(todos);
        }
    }
}
