using Core.Dto.Projects;

namespace Web.Viewmodels.Projects;

public class ProjectDetailsViewModel
{
    public bool CanEdit { get; set; }

    public ProjectDetailsDto ProjectDetailsDto { get; set; } = new();
}