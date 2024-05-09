using Core.Models.Identitiy;

namespace Core.Models.Projects;

public class Comment : BaseEntity
{
    public string Text { get; set; } = string.Empty;

    public bool IsPrivate { get; set; } = true;

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;
}