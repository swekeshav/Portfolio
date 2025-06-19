namespace Portfolio.Web.Models;

public class TodoViewModel
{
	public string Title { get; set; } = null!;
	public Guid UUID { get; set; }
	public bool IsCompleted { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? ReviewDate { get; set; }
}
