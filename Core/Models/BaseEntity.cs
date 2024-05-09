namespace Core.Models;

public class BaseEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Флаг актуальности.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Дата и время создания.
    /// </summary>
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Дата и время последнего обновления.
    /// </summary>
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
}