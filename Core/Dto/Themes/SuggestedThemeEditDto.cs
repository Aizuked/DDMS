using AutoMapper;
using Core.Models.Themes;

namespace Core.Dto.Themes;

public class SuggestedThemeEditDto
{
    public string Text { get; set; } = string.Empty;
}

public partial class SuggestedThemeEditDtoProfile : Profile
{
    public SuggestedThemeEditDtoProfile()
    {
        CreateMap<SuggestedThemeEditDto, SuggestedTheme>();
    }
}
