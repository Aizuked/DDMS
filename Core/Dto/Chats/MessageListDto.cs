using AutoMapper;
using Core.Models.Chats;

namespace Core.Dto.Chats;

public class MessageListDto
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime TimeStamp { get; set; }

    public bool IsEdited { get; set; }

    public bool IsReceived { get; set; }

    public int? LocalFileId { get; set; }

    public int? ProjectTaskId { get; set; }

    public string ProjectTaskDisplayName { get; set; } = string.Empty;

    public string ProjectTaskStatusDisplayName { get; set; } = string.Empty;
}

public partial class MessageListDtoProfile : Profile
{
    public MessageListDtoProfile()
    {
        CreateMap<Message, MessageListDto>();
    }
}
