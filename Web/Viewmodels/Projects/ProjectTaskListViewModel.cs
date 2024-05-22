using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectTaskListViewModel : ListBaseFilter
{
    public List<ProjectTaskListDto> ProjectTaskListDtos { get; set; } = [];
}