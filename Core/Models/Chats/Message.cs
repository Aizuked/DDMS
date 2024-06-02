using Core.Models.Files;
using Core.Models.Identity;
using Core.Models.Projects;

namespace Core.Models.Chats;

public class Message
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    public bool IsEdited { get; set; }

    public bool IsReceived { get; set; }

    public bool IsDeleted { get; set; }

    public int? LocalFileId { get; set; }

    public virtual LocalFile? LocalFile { get; set; }

    public int? ProjectTaskId { get; set; }

    public virtual ProjectTask? ProjectTask { get; set; }

    public int SenderId { get; set; }

    public virtual User Sender { get; set; } = null!;
}