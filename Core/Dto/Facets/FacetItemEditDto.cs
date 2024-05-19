using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetItemEditDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int FacetId { get; set; }
}

public partial class FacetItemEditDtoProfile : Profile
{
    public FacetItemEditDtoProfile()
    {
        CreateMap<FacetItemEditDto, FacetItem>();
    }
}
