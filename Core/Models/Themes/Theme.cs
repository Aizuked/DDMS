using Core.Models.Identity;

namespace Core.Models.Themes;

public class Theme : BaseEntity
{
    public bool IsApproved { get; set; }

    public int? SelectedThemeId { get; set; }

    public SuggestedTheme? SelectedTheme { get; set; }

    public int? SelectedThemeToChangeId { get; set; }

    public SuggestedTheme? SelectedThemeToChange { get; set; }

    public int? ApproverId { get; set; }

    public User? Approver { get; set; }
}