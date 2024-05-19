using AutoMapper;
using Core.Models.Files;

namespace Core.Dto.Files;

public class LocalFileGroupEditDto
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
        CreateMap<LocalFileGroupEditDto, LocalFileGroup>();
    }
}
