using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class ChatListViewModel : ListPaginationFilter
{
    public List<ChatListDto> ChatListDtos { get; set; } = [];
}