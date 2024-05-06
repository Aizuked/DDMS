using Core.Models.Identitiy;

namespace Core.Models.Questionnaires;

public class QuestionnaireResult : BaseEntity
{
    public int QuestionnaireId { get; set; }

    public virtual Questionnaire Questionnaire { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = null!;

    public int IntervieweeId { get; set; }

    public virtual User Interviewee { get; set; } = null!;
}