using Core.Dto.Facets;

namespace Web.Viewmodels.Facets;

public class FacetItemListViewModel : ListBaseFilter
{
    public List<FacetItemListDto> FacetItemListDtos { get; set; } = [];
}