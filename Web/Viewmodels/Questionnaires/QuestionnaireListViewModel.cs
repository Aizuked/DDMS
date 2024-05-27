using Core.Dto.Questionnaires;

namespace Web.Viewmodels.Questionnaires;

public class QuestionnaireListViewModel : ListPaginationFilter
{
    public List<QuestionnaireListDto> QuestionnaireListDtos { get; set; } = [];
}