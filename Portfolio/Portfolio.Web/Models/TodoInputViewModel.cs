using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Models;

public class TodoInputViewModel
{
	[Required]
	[Display(Name = "Title")]
	public string? Title { get; set; }
	[DataType(DataType.DateTime)]
	public DateTime? DueDate { get; set; }
	[DataType(DataType.DateTime)]
	public DateTime? ReviewDate { get; set; }

	public TodoViewModel ToTodo()
	{
		return new TodoViewModel
		{
			Title = Title ?? "Test Todo",
			UUID = Guid.NewGuid(),
			DueDate = DueDate,
			ReviewDate = ReviewDate
		};
	}
}
