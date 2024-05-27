using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectTaskListViewModel : ListPaginationFilter
{
    public List<ProjectTaskListDto> ProjectTaskListDtos { get; set; } = [];
}