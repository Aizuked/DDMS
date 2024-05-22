using AutoMapper;
using Core.Models.Chats;

namespace Core.Dto.Chats;

public class ChatEditDto : BaseEntityDto
{
    public int? ProjectId { get; set; }

    public ICollection<int> ParticipantIds { get; set; } = [];
}

public partial class ChatEditDtoProfile : Profile
{
    public ChatEditDtoProfile()
    {
        CreateMap<ChatEditDto, Chat>()
            .ForMember(i => i.Project, opt => opt.Ignore())
            .ForMember(i => i.Messages, opt => opt.Ignore())
            .ForMember(i => i.Participants, opt => opt.MapFrom(j => j.ParticipantIds));
    }
}
