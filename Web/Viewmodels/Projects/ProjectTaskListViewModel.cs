using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectTaskListViewModel : ListBaseViewModel
{
    public List<ProjectTaskListDto> ProjectTaskListDtos { get; set; } = [];
}