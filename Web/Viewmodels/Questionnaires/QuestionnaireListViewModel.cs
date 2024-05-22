using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireListViewModel : ListBaseFilter
{
    public List<QuestionnaireListDto> QuestionnaireListDtos { get; set; } = [];
}