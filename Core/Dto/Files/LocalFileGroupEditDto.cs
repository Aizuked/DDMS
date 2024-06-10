using AutoMapper;
using Core.Models.Files;

namespace Core.Dto.Files;

public class LocalFileGroupEditDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public long MaxSize { get; set; }

    public ICollection<string> AllowedMimeTypes { get; set; } = [];
}

public partial class LocalFileGroupEditDtoProfile : Profile
{
    public LocalFileGroupEditDtoProfile()
    {
        CreateMap<LocalFileGroupEditDto, LocalFileGroup>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Files, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
