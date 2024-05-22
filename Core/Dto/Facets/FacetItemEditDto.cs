using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetItemEditDto : BaseEntityDto
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
        CreateMap<FacetItemEditDto, FacetItem>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Facet, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore());
    }
}
