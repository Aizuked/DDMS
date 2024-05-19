using Core.Dto.Identity;

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

    public string? SelectedThemeText { get; set; }

    public bool SelectedThemeIsApproved { get; set; }

    public bool SelectedThemeIsChangeRequested { get; set; }
}