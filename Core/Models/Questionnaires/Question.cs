using Core.Models.Facets;

namespace Core.Models.Questionnaires;

public class Question : BaseEntity
{
    public int Text { get; set; }

    public int TypeId { get; set; }

    /// <summary>
    /// Перечисление QuestionType.
    /// </summary>
    public FacetItem Type { get; set; } = null!;

    public bool IsRequired { get; set; }
}

// public enum QuestionType
// {
//     Text = 0,
//
//     TimeSpan = 1,
//
//     Numeric = 2,
//
//     CheckBox = 3,
//
//     SelectList = 4,
//
//     MultiSelect = 5
// }