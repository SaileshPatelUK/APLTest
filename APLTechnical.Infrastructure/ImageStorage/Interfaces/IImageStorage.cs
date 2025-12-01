namespace APLTechnical.Infrastructure.ImageStorage.Interfaces;

public interface IImageStorage
{
    Task SaveAsync(string filename, Stream content, CancellationToken cancellationToken = default);
    Task<Stream> GetAsync(string filename, CancellationToken cancellationToken = default);
}
