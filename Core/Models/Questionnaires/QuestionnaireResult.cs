using Core.Models.Identity;

namespace Core.Models.Questionnaires;

public class QuestionnaireResult : BaseEntity
{
    public int IntervieweeId { get; set; }

    public virtual User Interviewee { get; set; } = null!;

    public int QuestionnaireId { get; set; }

    public virtual Questionnaire Questionnaire { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = [];
}