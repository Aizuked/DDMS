namespace Core.Models.Facets;

public class FacetItem : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int FacetId { get; set; }

    public virtual Facet Facet { get; set; } = null!;
}