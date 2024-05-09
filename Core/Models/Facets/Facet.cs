namespace Core.Models.Facets;

public class Facet : BaseEntity
{
    public bool IsSystem { get; set; }

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public virtual ICollection<FacetItem> FacetItems { get; set; } = [];
}