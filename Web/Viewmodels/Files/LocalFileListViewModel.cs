using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileListViewModel : ListBaseViewModel
{
    public List<LocalFileListDto> LocalFileListDtos { get; set; } = [];
}