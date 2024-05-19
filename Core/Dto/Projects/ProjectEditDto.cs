using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectEditDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public int StudentId { get; set; }

    public int TeacherId { get; set; }

    public int StatusId { get; set; }

    public int ThemeId { get; set; }
}

public partial class ProjectEditDtoProfile : Profile
{
    public ProjectEditDtoProfile()
    {
        CreateMap<ProjectEditDto, Project>();
    }
}
