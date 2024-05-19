using System.Text;
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
        var imageConfig = ImageConfig.FromConfiguration(_configuration);
        using var image = await Image.LoadAsync(stream);

        if (!imageConfig.IsInMaxBounds(image))
        {
            using var resizedImage =
                RestrictImageSize(
                    image,
                    (int)imageConfig.MaxWidth!,
                    (int)imageConfig.MaxHeight!
                );
            await resizedImage.SaveAsync(fullPath);
        }
        else
        {
            await image.SaveAsync(fullPath);
        }

        if (!imageConfig.IsInMinBounds(image))
        {
            using var resizedImage =
                RestrictImageSize(
                    image,
                    image.Size.Width / imageConfig.ThumbCoefficient ?? (int)imageConfig.ThumbMinWidth!,
                    image.Size.Height / imageConfig.ThumbCoefficient ?? (int)imageConfig.ThumbMinHeight!
                );
            await resizedImage.SaveAsync(AppendThumbPostfix(fullPath));
        }
    }

    private Image RestrictImageSize(Image image, int width, int height)
    {
        var resizeOptions = new ResizeOptions
        {
            Mode = ResizeMode.Min,
            Size = new Size(width, height)
        };

        image.Mutate(i => i.Resize(resizeOptions));

        return image;
    }

    private string AppendThumbPostfix(string fullPath)
    {
        var sb = new StringBuilder(fullPath);
        var lastDotInd = fullPath.LastIndexOf('.');
        sb.Insert(lastDotInd, Constants.IMAGES_THUMB_POSTFIX);
        return sb.ToString();
    }

    private class ImageConfig
    {
        public static ImageConfig FromConfiguration(IConfiguration configuration)
        {
            var imageConfig =
                configuration
                    .GetSection("Files")
                    .GetSection("Images")
                    .GetSection("Dimensions");

            return new ImageConfig
            {
                MaxWidth =
                    int.TryParse(imageConfig["MaxWidth"], out var maxWidth)
                        ? maxWidth
                        : null,
                MaxHeight =
                    int.TryParse(imageConfig["MaxHeight"], out var maxHeight)
                        ? maxHeight
                        : null,
                ThumbCoefficient =
                    int.TryParse(imageConfig["ThumbCoefficient"], out var thumbCoefficient)
                        ? thumbCoefficient
                        : null,
                ThumbMinWidth =
                    int.TryParse(imageConfig["ThumbMinWidth"], out var thumbMinWidth)
                        ? thumbMinWidth
                        : null,
                ThumbMinHeight =
                    int.TryParse(imageConfig["ThumbMinHeight"], out var thumbMinHeight)
                        ? thumbMinHeight
                        : null
            };
        }

        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public int? ThumbCoefficient { get; set; }
        public int? ThumbMinWidth { get; set; }
        public int? ThumbMinHeight { get; set; }

        public bool IsInMaxBounds(Image image)
        {
            return
                !MaxWidth.HasValue ||
                !MaxHeight.HasValue ||
                image.Size.Width <= MaxWidth &&
                image.Size.Height <= MaxHeight;
        }

        public bool IsInMinBounds(Image image)
        {
            if (
                !ThumbCoefficient.HasValue ||
                !ThumbMinWidth.HasValue ||
                !ThumbMinHeight.HasValue
            )
                return true;

            return
                image.Size.Width / ThumbCoefficient >= ThumbMinWidth &&
                image.Size.Height / ThumbCoefficient >= ThumbMinHeight;
        }
    }
}