using Core.Models.Files;

namespace Core.Services;

public interface IFileSystemService
{
    Task<LocalFile> StoreAsync(
        Stream stream,
        string fileExtension,
        int uploaderId
    );
}