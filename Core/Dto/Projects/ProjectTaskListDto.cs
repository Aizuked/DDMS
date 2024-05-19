namespace Core.Dto.Projects;

public class ProjectTaskListDto
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public int Readiness { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public string StatusDisplayName { get; set; } = string.Empty;
}