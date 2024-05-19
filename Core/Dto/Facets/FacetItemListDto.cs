namespace Core.Dto.Facets;

public class FacetItemListDto
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int FacetId { get; set; }

    public bool FacetIsSystem { get; set; }

    public string FacetCode { get; set; } = string.Empty;

    public string FacetDisplayName { get; set; } = string.Empty;

    public string FacetDescription { get; set; } = string.Empty;
}