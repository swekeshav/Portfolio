namespace Portfolio.Web.Models;

public class TriviaQuestionsList
{
    public int Response_Code { get; set; }
    public List<TriviaQuestionInfo> Results { get; set; } = null!;
}
