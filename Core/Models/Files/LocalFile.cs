using Core.Models.Identitiy;

namespace Core.Models.Files;

public class LocalFile : BaseEntity
{
    public string PhysicalPath { get; set; } = string.Empty;

    public string PhysicalName { get; set; } = string.Empty;

    public string? DisplayName { get; set; }

    public string? MimeType { get; set; }

    public long Size { get; set; }

    public int UploaderId { get; set; }

    public virtual User Uploader { get; set; } = null!;

    public int LocalFileGroupId { get; set; }

    public virtual LocalFileGroup LocalFileGroup { get; set; } = null!;
}