using AutoMapper;
using Core.Dto.Facets;
using Core.Dto.Identity;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireListDto
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public int ParticipationCount { get; set; }

    public FacetItemDetailsDto Type { get; set; } = new();

    public UserListDto Author { get; set; } = new();

    public ICollection<QuestionListDto> Questions { get; set; } = [];
}

public partial class QuestionnaireListDtoProfile : Profile
{
    public QuestionnaireListDtoProfile()
    {
        CreateMap<Questionnaire, QuestionnaireListDto>();
    }
}
