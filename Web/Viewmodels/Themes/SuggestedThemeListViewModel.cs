using Core.Dto.Themes;

namespace Web.Viewmodels.Themes;

public class SuggestedThemeListViewModel : ListBaseViewModel
{
    public List<SuggestedThemeListDto> SuggestedThemeListDtos { get; set; } = [];
}