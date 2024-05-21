using Core.Models.Facets;

namespace Core.Models.Questionnaires;

public class Question : BaseEntity
{
    public int Text { get; set; }

    public bool IsRequired { get; set; }

    public int TypeId { get; set; }

    /// <summary>
    /// Перечисление question_type.
    /// </summary>
    public virtual FacetItem Type { get; set; } = null!;
}
