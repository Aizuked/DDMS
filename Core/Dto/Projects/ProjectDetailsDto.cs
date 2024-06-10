using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectDetailsDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public UserListDto? Student { get; set; }

    public UserListDto? Teacher { get; set; }

    public string? TeacherJobTitle { get; set; }

    public string? StatusDisplayName { get; set; }

    public string? ThemeSelectedThemeText { get; set; }

    public int? ThemeId { get; set; }

    public bool? ThemeIsApproved { get; set; }
}

public partial class ProjectDetailsDtoProfile : Profile
{
    public ProjectDetailsDtoProfile()
    {
        CreateMap<Project, ProjectDetailsDto>();
    }
}
