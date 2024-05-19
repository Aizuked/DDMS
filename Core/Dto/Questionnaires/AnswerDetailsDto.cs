namespace Core.Dto.Questionnaires;

public class AnswerDetailsDto
{
    public string? Text { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public decimal? Number { get; set; }

    public bool? Checked { get; set; }

    public virtual ICollection<string>? MultiSelected { get; set; }

    public QuestionListDto Question { get; set; } = new();
}