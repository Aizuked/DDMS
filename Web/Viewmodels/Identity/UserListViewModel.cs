using Core.Dto.Identity;

namespace Web.Viewmodels.Identity;

public class UserListViewModel : ListPaginationFilter
{
    public List<UserListDto> UserListDtos { get; set; } = [];
}