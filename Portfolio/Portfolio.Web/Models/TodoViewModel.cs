namespace Portfolio.Web.Models;

public class TodoViewModel
{
    public IEnumerable<Todo> Todos { get; set; } = [];
    public TodoInputViewModel TodoInput { get; set; } = null!;
}
