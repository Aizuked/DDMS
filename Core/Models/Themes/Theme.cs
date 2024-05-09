using Core.Models.Identitiy;

namespace Core.Models.Themes;

public class Theme : BaseEntity
{
    public bool IsProcessed { get; set; }

    public bool IsApproved { get; set; }

    public bool IsChangeRequested { get; set; }

    public int? SelectedThemeId { get; set; }

    public SuggestedTheme? SelectedTheme { get; set; }

    public int? ApproverId { get; set; }

    public User? Approver { get; set; }

    public ICollection<SuggestedTheme> SuggestedThemes { get; set; } = [];

    public ICollection<KeyWord> KeyWords { get; set; } = [];
}