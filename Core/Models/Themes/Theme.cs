using Core.Models.Identitiy;

namespace Core.Models.Themes;

public class Theme : BaseEntity
{
    public KeyWord[] KeyWords { get; set; } = [];

    public bool IsProcessed { get; set; }

    public string[] SuggestedThemes { get; set; } = [];

    public string? Name { get; set; }

    public bool IsApproved { get; set; }

    public bool IsChangeRequested { get; set; }

    public int ApproverId { get; set; }

    public User Approver { get; set; } = null!;
}