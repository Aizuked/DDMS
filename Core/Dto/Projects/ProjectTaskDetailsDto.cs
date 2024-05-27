using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectTaskDetailsDto : BaseEntityDto
{
    public string DisplayName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Readiness { get; set; }

    public DateTime? DateTimeStart { get; set; }

    public DateTime? DateTimeEnd { get; set; }

    public ProjectListDto ProjectListDto { get; set; } = new();

    public ProjectTaskListDto? ParentTask { get; set; }

    public UserListDto Author { get; set; } = new();

    public ICollection<CommentDetailsDto> Comments { get; set; } = [];

    public ICollection<ProjectTaskListDto> LinkedTasks { get; set; } = [];

    public ICollection<int> LocalFilesIds { get; set; } = [];
}

public partial class ProjectTaskDetailsDtoProfile : Profile
{
    public ProjectTaskDetailsDtoProfile()
    {
        CreateMap<ProjectTask, ProjectTaskDetailsDto>()
            .ForMember(dto => dto.LocalFilesIds, opts => opts.MapFrom(entity => entity.LocalFiles.Select(i => i.Id)));
    }
}
