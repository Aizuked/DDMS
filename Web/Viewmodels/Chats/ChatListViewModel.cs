using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class ChatListViewModel : ListBaseViewModel
{
    public List<ChatListDto> ChatListDtos { get; set; } = [];
}