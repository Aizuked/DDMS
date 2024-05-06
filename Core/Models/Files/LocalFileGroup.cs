namespace Core.Models.Files;

public class LocalFileGroup : BaseEntity
{
    public string Code { get; set; } = string.Empty;

    public string? DisplayName { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>
    /// Максимальный размер файла в байтах. По умолчанию 5МБ.
    /// </summary>
    public long MaxSize { get; set; } = 5_242_880;

    public string[] AllowedMimeTypes { get; set; } = [];
}