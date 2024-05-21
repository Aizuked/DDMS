using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireResultListViewModel : ListBaseViewModel
{
    public List<QuestionnaireResultListDto> QuestionnaireResultListDtos { get; set; } = [];
}