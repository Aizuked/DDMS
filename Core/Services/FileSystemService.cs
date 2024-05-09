using Core.Models.Files;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Core.Services;

public class FileSystemService : IFileSystemService
{
    private readonly IConfiguration _configuration;

    public FileSystemService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<LocalFile> StoreAsync(Stream stream, int uploaderId, bool isImage = false)
    {
        var physicalPath = GetPhysicalPath(uploaderId);
        var physicalName = GetPhysicalName();
        var fullPath = physicalPath + physicalName;

        Directory.CreateDirectory(physicalPath);

        if (isImage)
            await ProcessImageAsync(stream, fullPath);

        await using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();

        return new LocalFile
        {
            PhysicalPath = fullPath
        };
    }

    private string GetPhysicalPath(int uploaderId)
    {
        return
            _configuration.GetSection("Files")["BasePath"] ??
            Constants.ENV_FILE_PATH + uploaderId + Constants.ENV_DIR_SEP;
    }

    private string GetPhysicalName()
    {
        return
            Guid.NewGuid()
                .ToString();
    }

    private async Task ProcessImageAsync(Stream stream, string fullPath)
    {
        using var resizedImage = RestrictImageSize(await Image.LoadAsync(stream));

        await resizedImage.SaveAsync(fullPath);
    }

    private Image RestrictImageSize(Image image)
    {
        var imageConfig =
            _configuration.GetSection("Files")
                          .GetSection("Images")
                          .GetSection("Dimensions");
        if (
            !int.TryParse(imageConfig["MaxWidth"], out var maxWidth) ||
            !int.TryParse(imageConfig["MaxHeight"], out var maxHeight) &&
            image.Size.Width <= maxWidth &&
            image.Size.Height <= maxHeight
        )
            return image;

        var resizeOptions = new ResizeOptions
        {
            Mode = ResizeMode.Min,
            Size = new Size(maxWidth, maxHeight)
        };

        image.Mutate(i => i.Resize(resizeOptions));

        return image;
    }
}