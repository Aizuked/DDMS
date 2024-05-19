using Core.Dto.Identity;

namespace Core.Dto.Projects;

public class ProjectListDto
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public UserListDto Student { get; set; } = new();

    public UserListDto Teacher { get; set; } = new();

    public string StatusDisplayName { get; set; } = string.Empty;
}