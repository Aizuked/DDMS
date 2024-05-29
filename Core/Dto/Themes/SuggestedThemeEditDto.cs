using AutoMapper;
using Core.Models.Themes;

namespace Core.Dto.Themes;

public class SuggestedThemeEditDto : BaseEntityDto
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public int UserId { get; set; }

    public List<KeyWordListDto> KeyWords { get; set; } = [];
}

public partial class SuggestedThemeEditDtoProfile : Profile
{
    public SuggestedThemeEditDtoProfile()
    {
        CreateMap<SuggestedThemeEditDto, SuggestedTheme>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.UserId, opt => opt.Ignore())
            .ForMember(i => i.User, opt => opt.Ignore());
    }
}
