using AutoMapper;
using Core.Dto.Identity;
using Core.Models.Chats;

namespace Core.Dto.Chats;

public class ChatListDto
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? ProjectCode { get; set; }

    public string? ProjectDisplayName { get; set; }

    public MessageListDto LastMessage { get; set; } = new();

    public ICollection<UserListDto> Participants { get; set; } = [];

    public DateTimeOffset Updated { get; set; }
}

public partial class ChatListDtoProfile : Profile
{
    public ChatListDtoProfile()
    {
        CreateMap<Chat, ChatListDto>()
            .ForMember(dto => dto.LastMessage, opts => opts.MapFrom(entity => entity.Messages.OrderByDescending(i => i.TimeStamp).FirstOrDefault()));
    }
}
