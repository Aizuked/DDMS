namespace Web.Viewmodels.Files;

public class LocalFileViewModel
{
    public ICollection<string> AllowedMimeTypes { get; set; } = [];
    public long MaxSize { get; set; }
}