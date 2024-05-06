using Core.Models.Identitiy;
using Core.Models.Projects;

namespace Core.Models.Chats;

public class Chat : BaseEntity
{
    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = null!;

    public virtual ICollection<User> Participants { get; set; } = null!;
}