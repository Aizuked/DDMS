using AutoMapper;
using Core.Models.Themes;

namespace Core.Dto.Themes;

public class KeyWordListDto : BaseEntityDto
{
    public string Word { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public bool IsProven { get; set; }
}

public partial class KeyWordListDtoProfile : Profile
{
    public KeyWordListDtoProfile()
    {
        CreateMap<KeyWord, KeyWordListDto>();
    }
}
