using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectListViewModel : ListPaginationFilter
{
    public List<ProjectListDto> ProjectListDtos { get; set; } = [];
}