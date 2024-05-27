using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileListViewModel : ListPaginationFilter
{
    public List<LocalFileListDto> LocalFileListDtos { get; set; } = [];
}