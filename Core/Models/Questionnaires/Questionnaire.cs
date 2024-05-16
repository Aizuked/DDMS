using Core.Models.Facets;
using Core.Models.Identity;

namespace Core.Models.Questionnaires;

public class Questionnaire : BaseEntity
{
    public string DisplayName { get; set; } = string.Empty;

    public int ParticipationCount { get; set; }

    public int TypeId { get; set; }

    /// <summary>
    /// Перечисление QuestionnaireType.
    /// </summary>
    public FacetItem Type { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = [];
}