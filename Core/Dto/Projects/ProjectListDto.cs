using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectListDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public UserListDto Student { get; set; } = new();

    public UserListDto Teacher { get; set; } = new();

    public string StatusDisplayName { get; set; } = string.Empty;
}

public partial class ProjectListDtoProfile : Profile
{
    public ProjectListDtoProfile()
    {
        CreateMap<Project, ProjectListDto>();
    }
}
