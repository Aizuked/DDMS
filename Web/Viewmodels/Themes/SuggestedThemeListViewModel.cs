using Core.Dto.Themes;

namespace Web.Viewmodels.Themes;

public class SuggestedThemeListViewModel : ListPaginationFilter
{
    public List<SuggestedThemeListDto> SuggestedThemeListDtos { get; set; } = [];
}