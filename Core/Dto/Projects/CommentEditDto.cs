using AutoMapper;
using Core.Models.Projects;

namespace Core.Dto.Projects;

public class CommentEditDto
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
        CreateMap<CommentEditDto, Comment>();
    }
}
