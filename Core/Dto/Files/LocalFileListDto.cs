using AutoMapper;
using Core.Models.Files;

namespace Core.Dto.Files;

public class LocalFileListDto
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;

    public long Size { get; set; }

    public LocalFileGroupListDto LocalFileGroup { get; set; } = new();
}

public partial class LocalFileListDtoProfile : Profile
{
    public LocalFileListDtoProfile()
    {
        CreateMap<LocalFile, LocalFileListDto>();
    }
}
