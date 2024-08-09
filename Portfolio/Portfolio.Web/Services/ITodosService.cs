using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITodosService
{
    Task<TodoViewModel> GetTodos();
}