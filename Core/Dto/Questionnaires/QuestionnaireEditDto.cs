using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireEditDto : BaseEntityDto
{
    public string DisplayName { get; set; } = string.Empty;

    public int TypeId { get; set; }

    public ICollection<int> QuestionIds { get; set; } = [];
}

public partial class QuestionnaireEditDtoProfile : Profile
{
    public QuestionnaireEditDtoProfile()
    {
        CreateMap<QuestionnaireEditDto, Questionnaire>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Type, opt => opt.Ignore())
            .ForMember(i => i.AuthorId, opt => opt.Ignore())
            .ForMember(i => i.Author, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForMember(i => i.ParticipationCount, opt => opt.Ignore())
            .ForMember(i => i.Questions, opt => opt.MapFrom(j => j.QuestionIds));
    }
}
