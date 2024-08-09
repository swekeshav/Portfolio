namespace Portfolio.Web.Models;

public class TodoViewModel
{
    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();
}
