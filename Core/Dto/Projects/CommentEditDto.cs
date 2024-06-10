using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class CommentEditDto : BaseEntityDto
{
    public string Text { get; set; } = string.Empty;

    public bool IsPrivate { get; set; }

    public int AuthorId { get; set; }

    public ICollection<int> LocalFiles { get; set; } = [];
}

public partial class CommentEditDtoProfile : Profile
{
    public CommentEditDtoProfile()
    {
        CreateMap<CommentEditDto, Comment>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.Author, opt => opt.Ignore())
            .ForMember(i => i.IsDeleted, opt => opt.Ignore())
            .ForMember(i => i.Created, opt => opt.Ignore())
            .ForMember(i => i.Updated, opt => opt.Ignore())
            .ForMember(i => i.LocalFiles, opt => opt.MapFrom(j => j.LocalFiles))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
