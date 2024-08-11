namespace Portfolio.Web.Models;

public class FrontPageViewModel
{
    public IEnumerable<TodoViewModel> Todos { get; set; } = null!;
    public TodoInputViewModel TodoInput { get; set; } = null!;
}
