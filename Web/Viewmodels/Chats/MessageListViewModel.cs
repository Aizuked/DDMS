using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class MessageListViewModel : ListBaseViewModel
{
    public List<MessageListDto> MessageListDtos { get; set; } = [];
}