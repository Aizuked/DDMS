using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class ProjectEditDto : BaseEntityDto
{
    public string Code { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public bool IsPublic { get; set; }

    public int StudentId { get; set; }

    public int TeacherId { get; set; }

    public int StatusId { get; set; }
}

public partial class ProjectEditDtoProfile : Profile
{
    public ProjectEditDtoProfile()
    {
        CreateMap<ProjectEditDto, Project>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Student, opt => opt.Ignore())
            .ForMember(i => i.Teacher, opt => opt.Ignore())
            .ForMember(i => i.Status, opt => opt.Ignore())
            .ForMember(i => i.Theme, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForMember(i => i.ThemeId, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
