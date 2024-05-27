using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileGroupListViewModel : ListPaginationFilter
{
    public List<LocalFileGroupListDto> LocalFileGroupListDtos { get; set; } = [];
}