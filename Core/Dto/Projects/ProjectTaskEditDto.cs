using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectTaskEditDto
{
    public string DisplayName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Readiness { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public int? ParentTaskId { get; set; }

    public int StatusId { get; set; }

    public int AuthorId { get; set; }

    public ICollection<int> LinkedTaskIds { get; set; } = [];

    public ICollection<int> LocalFileIdss { get; set; } = [];
}

public partial class ProjectTaskEditDtoProfile : Profile
{
    public ProjectTaskEditDtoProfile()
    {
        CreateMap<ProjectTaskEditDto, ProjectTask>();
    }
}
