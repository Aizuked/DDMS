using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class QuestionEditDto : BaseEntityDto
{
    public int Text { get; set; }

    public bool IsRequired { get; set; }

    public int TypeId { get; set; }
}

public partial class QuestionEditDtoProfile : Profile
{
    public QuestionEditDtoProfile()
    {
        CreateMap<QuestionEditDto, Question>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Type, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore());
    }
}
