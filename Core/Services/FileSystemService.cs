using Core.Models.Files;
using Microsoft.Extensions.Configuration;

namespace Core.Services;

public class FileSystemService : IFileSystemService
{
    private readonly IConfiguration _configuration;
    private readonly IFileProcessor _fileProcessor;

    public FileSystemService(
        IConfiguration configuration,
        IFileProcessor fileProcessor
    )
    {
        _configuration = configuration;
        _fileProcessor = fileProcessor;
    }

    public async Task<string> StoreAsync(
        Stream stream,
        string fileExtension,
        int uploaderId
    )
    {
        var physicalPath = GetPhysicalPath(uploaderId);
        var physicalName = GetPhysicalName();
        var fullPath = physicalPath + physicalName + '.' + fileExtension;

        Directory.CreateDirectory(physicalPath);

        await _fileProcessor.ProcessAsync(stream, fullPath);

        return fullPath;
    }

    private string GetPhysicalPath(
        int uploaderId
    )
    {
        return
            _configuration.GetSection("Files")["BasePath"] ??
            Constants.Constants.ENV_FILE_PATH + uploaderId + Constants.Constants.ENV_DIR_SEP;
    }

    private string GetPhysicalName()
    {
        return
            Guid.NewGuid()
                .ToString();
    }
}