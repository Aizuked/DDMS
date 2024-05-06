using Core.Models.Identitiy;

namespace Core.Models.Chats;

public class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public int? LocalFileId { get; set; }

    public string? LocalFileMimeType { get; set; }

    public DateTime TimeStamp { get; set; }

    public bool IsEdited { get; set; }

    public int SenderId { get; set; }

    public virtual User Sender { get; set; } = null!;
}