using AutoMapper;
using Core.Dto.Facets;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionListDto
{
    public int Id { get; set; }

    public int Text { get; set; }

    public bool IsRequired { get; set; }

    public int TypeId { get; set; }

    public FacetItemDetailsDto Type { get; set; } = new();
}

public partial class QuestionListDtoProfile : Profile
{
    public QuestionListDtoProfile()
    {
        CreateMap<Question, QuestionListDto>();
    }
}
