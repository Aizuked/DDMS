using Core.Dto.Identity;

namespace Web.Viewmodels.Identity;

public class UserListViewModel : ListBaseFilter
{
    public List<UserListDto> UserListDtos { get; set; } = [];
}