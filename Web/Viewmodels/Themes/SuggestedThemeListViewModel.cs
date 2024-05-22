using Core.Dto.Themes;

namespace Web.Viewmodels.Themes;

public class SuggestedThemeListViewModel : ListBaseFilter
{
    public List<SuggestedThemeListDto> SuggestedThemeListDtos { get; set; } = [];
}