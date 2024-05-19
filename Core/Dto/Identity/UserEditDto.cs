using AutoMapper;
using Core.Models.Identity;

namespace Core.Dto.Identity;

public class UserEditDto
{
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string? About { get; set; }

    public string? JobTitle { get; set; }

    public int? ProfilePictureId { get; set; }
}

public partial class UserEditDtoProfile : Profile
{
    public UserEditDtoProfile()
    {
        CreateMap<UserEditDto, User>();
    }
}
