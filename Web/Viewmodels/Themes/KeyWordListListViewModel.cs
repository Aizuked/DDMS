using Core.Dto.Themes;

namespace Web.Viewmodels.Themes;

public class KeyWordListListViewModel : ListPaginationFilter
{
    public List<KeyWordListDto> KeyWordListDtos { get; set; } = [];
}