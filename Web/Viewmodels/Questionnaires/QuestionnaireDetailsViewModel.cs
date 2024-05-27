using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireDetailsViewModel
{
    public bool CanEdit { get; set; }

    public QuestionnaireDetailsDto QuestionnaireDetailsDto { get; set; } = new();
}