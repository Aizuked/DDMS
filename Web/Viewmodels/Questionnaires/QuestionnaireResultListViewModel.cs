using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireResultListViewModel : ListBaseFilter
{
    public List<QuestionnaireResultListDto> QuestionnaireResultListDtos { get; set; } = [];
}