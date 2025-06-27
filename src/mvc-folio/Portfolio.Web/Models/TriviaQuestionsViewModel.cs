using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Web.Models;

public class TriviaQuestionsViewModel
{
	public List<SelectListItem> Categories { get; set; } = null!;
	public List<SelectListItem> Difficulties { get; set; } = null!;
}
