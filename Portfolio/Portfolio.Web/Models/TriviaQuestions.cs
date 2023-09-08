namespace Portfolio.Web.Models;

public class TriviaQuestions
{
	public int Response_Code { get; set; }
	public IEnumerable<TriviaQuestionInfo> Results { get; set; } = null!;
}
