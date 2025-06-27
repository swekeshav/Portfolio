using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITodosService
{
    Task AddTodo(TodoInputViewModel newTodo);
    Task<FrontPageViewModel> GetTodos();
    Task ToggleStatus(TodoStatusViewModel todoStatus);
}