using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectListViewModel : ListBaseViewModel
{
    public List<ProjectListDto> ProjectDetailsDtos { get; set; } = [];
}