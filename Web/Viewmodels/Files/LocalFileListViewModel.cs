using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileListViewModel : ListBaseFilter
{
    public List<LocalFileListDto> LocalFileListDtos { get; set; } = [];
}