using Core.Models.Identity;

namespace Core.Models.Files;

public class LocalFile : BaseEntity
{
    public string PhysicalPath { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string MimeType { get; set; } = string.Empty;

    public long Size { get; set; }

    public int UploaderId { get; set; }

    public virtual User Uploader { get; set; } = null!;

    public int LocalFileGroupId { get; set; }

    public virtual LocalFileGroup LocalFileGroup { get; set; } = null!;
}