using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Core.Services;

public class ImageFileProcessor : IFileProcessor
{
    private readonly IConfiguration _configuration;

    public ImageFileProcessor(
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }

    public async Task ProcessAsync(
        Stream stream,
        string fullPath
    )
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