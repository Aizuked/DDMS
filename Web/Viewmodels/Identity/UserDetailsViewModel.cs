using Core.Dto.Identity;

namespace Web.Viewmodels.Identity;

public class UserDetailsViewModel
{
    public bool IsSelf { get; set; }

    public List<string> CurrentUserRole { get; set; } = [];

    public UserDetailsDto UserDetailsDto { get; set; } = new();
}