using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectTaskDetailsViewModel
{
    public bool CanEdit { get; set; }

    public ProjectTaskDetailsDto ProjectTaskDetailsDto { get; set; } = new();
}