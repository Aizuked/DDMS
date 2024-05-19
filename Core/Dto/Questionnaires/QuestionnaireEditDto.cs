using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireEditDto
{
    public string DisplayName { get; set; } = string.Empty;

    public int TypeId { get; set; }

    public ICollection<int> Questions { get; set; } = [];
}

public partial class QuestionnaireEditDtoProfile : Profile
{
    public QuestionnaireEditDtoProfile()
    {
        CreateMap<QuestionnaireEditDto, Questionnaire>();
    }
}
