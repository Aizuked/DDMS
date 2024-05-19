using AutoMapper;
using Core.Models.Chats;

namespace Core.Dto.Chats;

public class MessageEditDto
{
    public string? Content { get; set; }

    public bool IsDeleted { get; set; }

    public int? LocalFileId { get; set; }

    public int? ProjectTaskId { get; set; }
}

public partial class MessageEditDtoProfile : Profile
{
    public MessageEditDtoProfile()
    {
        CreateMap<MessageEditDto, Message>();
    }
}
