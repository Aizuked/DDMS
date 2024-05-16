namespace Core.Services;

public interface IFileProcessor
{
    Task ProcessAsync(Stream stream, string fullPath);
}