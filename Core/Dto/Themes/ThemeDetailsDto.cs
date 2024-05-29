using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Themes;

namespace Core.Dto.Themes;

public class ThemeDetailsDto : BaseEntityDto
{
    public bool IsApproved { get; set; }

    public bool IsChangeRequested { get; set; }

    public int? SelectedThemeId { get; set; }

    public string SelectedThemeText { get; set; } = string.Empty;

    public int? SelectedThemeToChangeId { get; set; }

    public string SelectedThemeToChangeText { get; set; } = string.Empty;

    public int? ApproverId { get; set; }

    public UserListDto? Approver { get; set; }

    public virtual ICollection<KeyWordListDto> KeyWords { get; set; } = [];
}

public partial class ThemeDetailsDtoProfile : Profile
{
    public ThemeDetailsDtoProfile()
    {
        CreateMap<Theme, ThemeDetailsDto>();
    }
}
