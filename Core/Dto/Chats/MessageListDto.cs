using AutoMapper;
using Core.Models.Chats;

namespace Core.Dto.Chats;

public class MessageListDto
{
    public long Id { get; set; }

    public int ChatId { get; set; }

    public string? Content { get; set; }

    public DateTime TimeStamp { get; set; }

    public bool IsEdited { get; set; }

    public bool IsReceived { get; set; }

    public int? LocalFileId { get; set; }

    public string? LocalFilePhysicalPath { get; set; }

    public string? LocalFileDisplayName { get; set; }

    public int? ProjectTaskId { get; set; }

    public string ProjectTaskDisplayName { get; set; } = string.Empty;

    public string ProjectTaskStatusDisplayName { get; set; } = string.Empty;

    public int SenderId { get; set; }

    public string? SenderDetailsPath { get; set; }

    public string? SenderProfilePicturePath { get; set; }

    public MessageListDto SetProfilePicturePath(string path)
    {
        this.SenderProfilePicturePath = path;
        return this;
    }
}

public partial class MessageListDtoProfile : Profile
{
    public MessageListDtoProfile()
    {
        CreateMap<Message, MessageListDto>()
            .ForMember(dto => dto.ChatId, opts => opts.Ignore())
            .ForMember(dto => dto.SenderDetailsPath, opts => opts.MapFrom(entity => $"/Identity/User/Details{entity.SenderId}"))
            .ForMember(dto => dto.SenderProfilePicturePath, opts => opts.Ignore());
    }
}
