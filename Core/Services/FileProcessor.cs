namespace Core.Services;

public class FileProcessor : IFileProcessor
{
    public async Task ProcessAsync(
        Stream stream,
        string fullPath
    )
    {
        await using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
    }
}