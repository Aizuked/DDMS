using Core.Dto.Files;

namespace Web.Viewmodels.Files;

public class LocalFileGroupListViewModel : ListBaseFilter
{
    public List<LocalFileGroupListDto> LocalFileGroupListDtos { get; set; } = [];
}