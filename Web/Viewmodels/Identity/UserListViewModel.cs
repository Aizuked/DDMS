using Core.Dto.Identity;

namespace Web.Viewmodels.Identity;

public class UserListViewModel : ListBaseViewModel
{
    public List<UserListDto> UserListDtos { get; set; } = [];
}