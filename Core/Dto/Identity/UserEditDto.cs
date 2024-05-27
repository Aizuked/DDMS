using AutoMapper;
using Core.Models.Identity;

namespace Core.Dto.Identity;

public class UserEditDto : BaseEntityDto
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
        CreateMap<UserEditDto, User>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.IsOnline, opt => opt.Ignore())
            .ForMember(i => i.LastOnline, opt => opt.Ignore())
            .ForMember(i => i.UserChats, opt => opt.Ignore())
            .ForMember(i => i.LocalFiles, opt => opt.Ignore())
            .ForMember(i => i.Questionnaires, opt => opt.Ignore())
            .ForMember(i => i.QuestionnaireResults, opt => opt.Ignore())
            .ForMember(i => i.UserName, opt => opt.Ignore())
            .ForMember(i => i.NormalizedUserName, opt => opt.Ignore())
            .ForMember(i => i.Email, opt => opt.Ignore())
            .ForMember(i => i.NormalizedEmail, opt => opt.Ignore())
            .ForMember(i => i.EmailConfirmed, opt => opt.Ignore())
            .ForMember(i => i.PasswordHash, opt => opt.Ignore())
            .ForMember(i => i.SecurityStamp, opt => opt.Ignore())
            .ForMember(i => i.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(i => i.PhoneNumber, opt => opt.Ignore())
            .ForMember(i => i.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(i => i.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(i => i.LockoutEnd, opt => opt.Ignore())
            .ForMember(i => i.LockoutEnabled, opt => opt.Ignore())
            .ForMember(i => i.AccessFailedCount, opt => opt.Ignore());
    }
}
