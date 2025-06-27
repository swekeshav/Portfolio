namespace Portfolio.Web.Models;

public class TriviaQuestionsSearchFilter
{
	public string Category { get; set; } = null!;
	public string Difficulty { get; set; } = null!;
	public int QuestionCount { get; set; }
}
