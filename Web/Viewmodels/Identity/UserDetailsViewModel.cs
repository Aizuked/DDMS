using Core.Dto.Identity;

namespace Web.Viewmodels.Identity;

public class UserDetailsViewModel
{
    public bool CanEdit { get; set; }

    public bool CanModifyRoles { get; set; }

    public UserDetailsDto UserDetailsDto { get; set; } = new();
}