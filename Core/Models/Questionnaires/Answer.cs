namespace Core.Models.Questionnaires;

public class Answer : BaseEntity
{
    public string Text { get; set; } = string.Empty;

    public DateTime DateTimeStart { get; set; }

    public DateTime DateTimeEnd { get; set; }

    public decimal Number { get; set; }

    public bool Checked { get; set; }

    public string[] MultiSelected { get; set; } = [];

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}