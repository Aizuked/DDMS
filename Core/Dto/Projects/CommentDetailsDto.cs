using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class CommentDetailsDto : BaseEntityDto
{
    public string Text { get; set; } = string.Empty;

    public bool IsPrivate { get; set; } = true;

    public UserListDto Author { get; set; } = new();

    public ICollection<int> LocalFilesIds { get; set; } = [];
}

public partial class CommentDetailsDtoProfile : Profile
{
    public CommentDetailsDtoProfile()
    {
        CreateMap<Comment, CommentDetailsDto>()
            .ForMember(dto => dto.LocalFilesIds, opts => opts.MapFrom(entity => entity.LocalFiles.Select(i => i.Id)));
    }
}
