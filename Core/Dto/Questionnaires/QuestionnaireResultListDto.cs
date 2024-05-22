using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireResultListDto : BaseEntityDto
{
    public UserListDto Interviewee { get; set; } = new();

    public QuestionnaireDetailsDto Questionnaire { get; set; } = new();

    public ICollection<AnswerDetailsDto> Answers { get; set; } = [];
}

public partial class QuestionnaireResultListDtoProfile : Profile
{
    public QuestionnaireResultListDtoProfile()
    {
        CreateMap<QuestionnaireResult, QuestionnaireResultListDto>();
    }
}
