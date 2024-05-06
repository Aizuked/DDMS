using Core.Models.Facets;
using Core.Models.Identitiy;
using Core.Models.Themes;

namespace Core.Models.Projects;

public class Project : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public int StudentId { get; set; }

    public virtual User Student { get; set; } = null!;

    public int TeacherId { get; set; }

    public virtual User Teacher { get; set; } = null!;

    public int StatusId { get; set; }

    /// <summary>
    /// Перечисление ProjectStatus.
    /// </summary>
    public FacetItem Status { get; set; } = null!;

    public bool IsPublic { get; set; }

    public int ThemeId { get; set; }

    public virtual Theme Theme { get; set; } = null!;
}