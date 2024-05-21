using Core.Dto.Facets;

namespace Web.Viewmodels.Facets;

public class FacetItemListViewModel : ListBaseViewModel
{
    public List<FacetItemListDto> FacetItemListDtos { get; set; } = [];
}