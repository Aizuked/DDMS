using Core.Models.Facets;
using Core.Models.Files;
using Core.Models.Identity;

namespace Core.Models.Projects;

public class ProjectTask : BaseEntity
{
    public string DisplayName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Readiness { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public int? ParentTaskId { get; set; }

    public virtual ProjectTask? ParentTask { get; set; }

    public int StatusId { get; set; }

    /// <summary>
    /// Перечисление task_status.
    /// </summary>
    public virtual FacetItem Status { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = [];

    public virtual ICollection<ProjectTask> LinkedTasks { get; set; } = [];

    public virtual ICollection<LocalFile> LocalFiles { get; set; } = [];
}