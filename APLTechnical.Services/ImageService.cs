using APLTechnical.Core.Interfaces;
using APLTechnical.Core.Models;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.Extensions.Logging;

namespace APLTechnical.Services;

public class ImageService(IImageStorage imageStorage, ILogger<ImageService> logger) : IImageService
{
    private readonly IImageStorage _imageStorage = imageStorage;
    private readonly ILogger<ImageService> _logger = logger;

    public Task<Images> GetNewImageIdAsync(string filename)
    {
        // your existing implementation
        throw new NotImplementedException();
    }

    public async Task SaveNewImageAsync(
        string filename,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("Filename must be provided.", nameof(filename));
        }

        ArgumentNullException.ThrowIfNull(content);

        _logger.LogInformation("Saving new image {FileName}", filename);
        await _imageStorage.SaveAsync(filename, content, cancellationToken).ConfigureAwait(false);
    }
}
