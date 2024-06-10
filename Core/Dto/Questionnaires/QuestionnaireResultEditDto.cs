using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireResultEditDto : BaseEntityDto
{
    public int QuestionnaireId { get; set; }

    public ICollection<int> AnswerIds { get; set; } = [];
}

public partial class QuestionnaireResultEditDtoProfile : Profile
{
    public QuestionnaireResultEditDtoProfile()
    {
        CreateMap<QuestionnaireResultEditDto, QuestionnaireResult>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.IntervieweeId, opt => opt.Ignore())
            .ForMember(i => i.Interviewee, opt => opt.Ignore())
            .ForMember(i => i.Questionnaire, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForMember(i => i.Answers, opt => opt.MapFrom(j => j.AnswerIds))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
