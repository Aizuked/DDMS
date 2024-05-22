using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class ChatListViewModel : ListBaseFilter
{
    public List<ChatListDto> ChatListDtos { get; set; } = [];
}