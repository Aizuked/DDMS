using Core.Models.Identity;

namespace Core.Models.Themes;

public class SuggestedTheme
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public int UserId { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<KeyWord> KeyWords { get; set; } = [];
}