using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetItemListDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int FacetId { get; set; }

    public bool FacetIsSystem { get; set; }

    public string FacetCode { get; set; } = string.Empty;

    public string FacetDisplayName { get; set; } = string.Empty;

    public string FacetDescription { get; set; } = string.Empty;
}

public partial class FacetItemListDtoProfile : Profile
{
    public FacetItemListDtoProfile()
    {
        CreateMap<FacetItem, FacetItemListDto>();
    }
}
