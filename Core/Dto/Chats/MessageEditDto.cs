﻿using AutoMapper;
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
        CreateMap<MessageEditDto, Message>()
            .ForMember(i => i.Id, opt => opt.Ignore())
            .ForMember(i => i.TimeStamp, opt => opt.Ignore())
            .ForMember(i => i.IsEdited, opt => opt.Ignore())
            .ForMember(i => i.IsReceived, opt => opt.Ignore())
            .ForMember(i => i.ProjectTask, opt => opt.Ignore())
            .ForMember(i => i.LocalFile, opt => opt.Ignore())
            .ForMember(i => i.SenderId, opt => opt.Ignore())
            .ForMember(i => i.Sender, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}
