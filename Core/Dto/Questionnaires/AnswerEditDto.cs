using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class AnswerEditDto : BaseEntityDto
{
    public string? Text { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public decimal? Number { get; set; }

    public bool? Checked { get; set; }

    public ICollection<string> MultiSelected { get; set; } = [];
}

public partial class AnswerEditDtoProfile : Profile
{
    public AnswerEditDtoProfile()
    {
        CreateMap<AnswerEditDto, Answer>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.QuestionId, opt => opt.Ignore())
            .ForMember(i => i.Question, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
