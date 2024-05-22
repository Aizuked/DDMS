using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class MessageListViewModel : ListBaseFilter
{
    public List<MessageListDto> MessageListDtos { get; set; } = [];
}