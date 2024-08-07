﻿using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public class TodosService : ITodosService
{
    public async Task<TodoViewModel> GetTodos()
    {
        await Task.Delay(1);

        var todos = new List<Todo>();
        for (var i = 0; i < 10; i++)
        {
            todos.Add(new Todo
            {
                Id = i,
                Title = $"Todo {i}",
                Description = $"Description for Todo {i}",
                IsCompleted = i % 2 == 0
            });
        }

        return new TodoViewModel
        {
            Todos = todos
        };
    }
}
