using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireResultListViewModel : ListPaginationFilter
{
    public List<QuestionnaireResultListDto> QuestionnaireResultListDtos { get; set; } = [];
}