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
        await Task.Delay(1);

        //var todos = new List<Todo>();
        //for (var i = 0; i < 10; i++)
        //{
        //    todos.Add(new Todo
        //    {
        //        Id = i,
        //        Title = $"Todo {i}",
        //        Description = $"Description for Todo {i}",
        //        IsCompleted = i % 2 == 0
        //    });
        //}

        return new FrontPageViewModel
        {
            Todos = TodosSet
        };
    }
}
