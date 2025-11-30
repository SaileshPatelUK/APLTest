namespace APLTechnical.Infrastructure.ImageStorage.Interfaces;

public interface IImageStorage
{
    Task SaveAsync(string path, string? blobName, Stream content, CancellationToken cancellationToken = default);
    Task<Stream> GetAsync(string path, string? blobName, CancellationToken cancellationToken = default);
}
