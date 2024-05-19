namespace Core.Models.Questionnaires;

public class Answer : BaseEntity
{
    public string? Text { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public decimal? Number { get; set; }

    public bool? Checked { get; set; }

    public virtual ICollection<string> MultiSelected { get; set; } = [];

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}