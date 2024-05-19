using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetEditDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

public partial class FacetEditDtoProfile : Profile
{
    public FacetEditDtoProfile()
    {
        CreateMap<FacetEditDto, Facet>();
    }
}
