using Core.Dto.Chats;

namespace Web.Viewmodels.Chats;

public class MessageListViewModel : ListPaginationFilter
{
    public List<MessageListDto> MessageListDtos { get; set; } = [];
}