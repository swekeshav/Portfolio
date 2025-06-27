using Portfolio.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Tests.ViewModels;

[Category(TraitNames.Todos)]
internal class TodoInputViewModelTests
{
	[Test]
	public void Validate_ShouldReturnError_WhenReviewDateAfterDueDate()
	{
		// Arrange
		var todoInput = new TodoInputViewModel
		{
			Title = "Test Todo",
			DueDate = DateTime.Now.AddDays(1),
			ReviewDate = DateTime.Now.AddDays(2) // Review date is after due date
		};

		var validationContext = new ValidationContext(todoInput);

		// Act
		var results = todoInput.Validate(validationContext).ToList();

		// Assert
		Assert.That(results, Has.Count.EqualTo(1));
		Assert.That(results[0].ErrorMessage, Is.EqualTo("Review date cannot be after due date."));
	}
}
