namespace Core.Dto.Chats;

public class ChatEditDto
{
    public int? ProjectId { get; set; }

    public ICollection<int> ParticipantIds { get; set; } = [];
}