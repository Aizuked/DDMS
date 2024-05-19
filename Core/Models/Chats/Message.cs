using Core.Models.Projects;

namespace Core.Models.Chats;

public class Message
{
    public long Id { get; set; }

    public string? Content { get; set; } = string.Empty;

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    public bool IsEdited { get; set; }

    public bool IsReceived { get; set; }

    public bool IsDeleted { get; set; }

    public int? LocalFileId { get; set; }

    public int? ProjectTaskId { get; set; }

    public virtual ProjectTask? ProjectTask { get; set; }
}