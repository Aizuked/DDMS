using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectListViewModel : ListBaseFilter
{
    public List<ProjectListDto> ProjectDetailsDtos { get; set; } = [];
}