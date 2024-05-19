using Core.Dto.Facets;
using Core.Dto.Identity;

namespace Core.Dto.Questionnaires;

public class QuestionnaireListDto
{
    public int Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public int ParticipationCount { get; set; }

    public FacetItemDetailsDto Type { get; set; } = new();

    public UserListDto Author { get; set; } = new();

    public ICollection<QuestionListDto> Questions { get; set; } = [];
}