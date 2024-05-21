using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileGroupListViewModel : ListBaseViewModel
{
    public List<LocalFileGroupListDto> LocalFileGroupListDtos { get; set; } = [];
}