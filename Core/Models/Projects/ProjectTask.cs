using Core.Models.Facets;
using Core.Models.Identitiy;

namespace Core.Models.Projects;

public class ProjectTask : BaseEntity
{
    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int StatusId { get; set; }

    /// <summary>
    /// Перечисление TaskStatus.
    /// </summary>
    public virtual FacetItem Status { get; set; } = null!;

    public int Readiness { get; set; }

    /// <summary>
    /// Чем выше - тем ниже по дереву задача.
    /// </summary>
    public int HierarchyLevel { get; set; }

    public DateTime DateTimeStart { get; set; }

    public DateTime DateTimeEnd { get; set; }

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; } = null!;

    public ICollection<ProjectTask> LinkedTasks { get; set; } = null!;
}