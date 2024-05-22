using AutoMapper;
using Core.Models.Questionnaires;

namespace Core.Dto.Questionnaires;

public class AnswerDetailsDto : BaseEntityDto
{
    public string? Text { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public decimal? Number { get; set; }

    public bool? Checked { get; set; }

    public ICollection<string> MultiSelected { get; set; } = [];

    public QuestionListDto Question { get; set; } = new();
}

public partial class AnswerDetailsDtoProfile : Profile
{
    public AnswerDetailsDtoProfile()
    {
        CreateMap<Answer, AnswerDetailsDto>();
    }
}
