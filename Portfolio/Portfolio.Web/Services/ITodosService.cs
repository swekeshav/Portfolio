using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITodosService
{
    void AddTodo(TodoInputViewModel newTodo);
    Task<TodoViewModel> GetTodos();
}