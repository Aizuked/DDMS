using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireListViewModel : ListBaseViewModel
{
    public List<QuestionnaireListDto> QuestionnaireListDtos { get; set; } = [];
}