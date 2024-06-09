using AutoMapper;
using Core.Models.Identity;

namespace Core.Dto.Identity;

public class UserListDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public bool IsOnline { get; set; }

    public DateTime? LastOnline { get; set; }

    public int? ProfilePicturePath { get; set; }
}

public partial class UserListDtoProfile : Profile
{
    public UserListDtoProfile()
    {
        CreateMap<User, UserListDto>()
            .ForMember(dto => dto.ProfilePicturePath, opt => opt.Ignore())
            .ForMember(dto => dto.FullName, opt => opt.MapFrom(entity => entity.LastName + entity.MiddleName + ' ' + entity.FirstName + ' '));
    }
}
