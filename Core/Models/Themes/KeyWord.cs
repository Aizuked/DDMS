namespace Core.Models.Themes;

public class KeyWord : BaseEntity
{
    public string Word { get; set; } = string.Empty;

    /// <summary>
    /// Допущено на общее обозрение.
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// Уже используется в теме диплома по приказу.
    /// </summary>
    public bool IsProven { get; set; }
}