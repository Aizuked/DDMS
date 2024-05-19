using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectDetailsDto
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public UserListDto Student { get; set; } = new();

    public UserListDto Teacher { get; set; } = new();

    public string? TeacherJobTitle { get; set; }

    public string StatusDisplayName { get; set; } = string.Empty;

    public string? ThemeSelectedThemeText { get; set; }

    public bool ThemeIsApproved { get; set; }

    public bool ThemeIsChangeRequested { get; set; }
}

public partial class ProjectDetailsDtoProfile : Profile
{
    public ProjectDetailsDtoProfile()
    {
        CreateMap<Project, ProjectDetailsDto>();
    }
}
