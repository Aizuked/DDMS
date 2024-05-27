namespace Core.Services;

public interface IFileSystemService
{
    Task<string> StoreAsync(
        Stream stream,
        string fileExtension,
        int uploaderId
    );
}