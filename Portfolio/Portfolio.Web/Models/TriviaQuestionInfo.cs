namespace Portfolio.Web.Models;

public class TriviaQuestionInfo
{
    public string Category { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Difficulty { get; set; } = null!;
    public string Question { get; set; } = null!;
    public string Correct_Answer { get; set; } = null!;
    public List<string> Incorrect_Answers { get; set; } = null!;
}
