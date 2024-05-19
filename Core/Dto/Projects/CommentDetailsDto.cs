using Core.Dto.Identity;

namespace Core.Dto.Projects;

public class CommentDetailsDto
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string Text { get; set; } = string.Empty;

    public bool IsPrivate { get; set; } = true;

    public UserListDto Author { get; set; } = new();

    public ICollection<int> LocalFilesIds { get; set; } = [];
}