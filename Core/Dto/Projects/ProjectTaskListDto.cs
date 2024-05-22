using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectTaskListDto : BaseEntityDto
{
    public string DisplayName { get; set; } = string.Empty;

    public int Readiness { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public string StatusDisplayName { get; set; } = string.Empty;
}

public partial class ProjectTaskListDtoProfile : Profile
{
    public ProjectTaskListDtoProfile()
    {
        CreateMap<ProjectTask, ProjectTaskListDto>();
    }
}
