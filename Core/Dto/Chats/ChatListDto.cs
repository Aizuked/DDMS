using Core.Dto.Identity;

namespace Core.Dto.Chats;

public class ChatListDto
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? ProjectCode { get; set; }

    public string? ProjectDisplayName { get; set; }

    public MessageListDto LastMessage { get; set; } = new();

    public ICollection<UserListDto> Participants { get; set; } = [];

    public DateTime Updated { get; set; }
}