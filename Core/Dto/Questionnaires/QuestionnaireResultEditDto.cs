using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionnaireResultEditDto
{
    public int QuestionnaireId { get; set; }

    public ICollection<int> Answers { get; set; } = [];
}

public partial class QuestionnaireResultEditDtoProfile : Profile
{
    public QuestionnaireResultEditDtoProfile()
    {
        CreateMap<QuestionnaireResultEditDto, QuestionnaireResult>();
    }
}
