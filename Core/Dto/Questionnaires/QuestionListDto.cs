using Core.Dto.Facets;

namespace Core.Dto.Questionnaires;

public class QuestionListDto
{
    public int Id { get; set; }

    public int Text { get; set; }

    public bool IsRequired { get; set; }

    public int TypeId { get; set; }

    public FacetItemDetailsDto Type { get; set; } = new();
}