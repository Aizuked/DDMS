using Core.Dto.Facets;

namespace Web.Viewmodels.Facets;

public class FacetItemListViewModel : ListPaginationFilter
{
    public List<FacetItemListDto> FacetItemListDtos { get; set; } = [];
}