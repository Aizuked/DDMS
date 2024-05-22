using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectTaskEditDto : BaseEntityDto
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

    public ICollection<int> LocalFileIds { get; set; } = [];
}

public partial class ProjectTaskEditDtoProfile : Profile
{
    public ProjectTaskEditDtoProfile()
    {
        CreateMap<ProjectTaskEditDto, ProjectTask>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.ParentTask, opt => opt.Ignore())
            .ForMember(i => i.Status, opt => opt.Ignore())
            .ForMember(i => i.Author, opt => opt.Ignore())
            .ForMember(i => i.Comments, opt => opt.Ignore())
            .ForMember(i => i.LinkedTasks, opt => opt.MapFrom(j => j.LocalFileIds))
            .ForMember(i => i.LocalFiles, opt => opt.MapFrom(j => j.LocalFileIds))
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore());
    }
}
