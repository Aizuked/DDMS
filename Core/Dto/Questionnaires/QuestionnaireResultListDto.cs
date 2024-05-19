using Core.Dto.Identity;

namespace Core.Dto.Questionnaires;

public class QuestionnaireResultListDto
{
    public int Id { get; set; }

    public UserListDto Interviewee { get; set; } = new();

    public QuestionnaireDetailsDto Questionnaire { get; set; } = new();

    public virtual ICollection<AnswerDetailsDto> Answers { get; set; } = [];
}