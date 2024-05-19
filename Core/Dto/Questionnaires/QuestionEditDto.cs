using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionEditDto
{
    public int Text { get; set; }

    public bool IsRequired { get; set; }

    public int TypeId { get; set; }
}

public partial class QuestionEditDtoProfile : Profile
{
    public QuestionEditDtoProfile()
    {
        CreateMap<QuestionEditDto, Question>();
    }
}
