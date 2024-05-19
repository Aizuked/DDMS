using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetItemDetailsDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
}

public partial class FacetItemDetailsDtoProfile : Profile
{
    public FacetItemDetailsDtoProfile()
    {
        CreateMap<FacetItem, FacetItemDetailsDto>();
    }
}
