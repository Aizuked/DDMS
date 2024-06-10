using AutoMapper;
using Core.Models.Facets;

namespace Core.Dto.Facets;

public class FacetEditDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

public partial class FacetEditDtoProfile : Profile
{
    public FacetEditDtoProfile()
    {
        CreateMap<FacetEditDto, Facet>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.IsSystem, opt => opt.Ignore())
            .ForMember(i => i.FacetItems, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
