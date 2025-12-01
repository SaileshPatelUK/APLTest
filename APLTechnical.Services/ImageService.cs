using APLTechnical.Core.Interfaces;
using APLTechnical.Core.Models;
using APLTechnical.Infrastructure.DataStorage.Interfaces;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.Extensions.Logging;

namespace APLTechnical.Services;

public class ImageService(IImageStorage imageStorage, IImageRepository imageRepository, ILogger<ImageService> logger) : IImageService
{
    private readonly IImageStorage _imageStorage = imageStorage;
    private readonly IImageRepository _imageRepository = imageRepository;
    private readonly ILogger<ImageService> _logger = logger;

    public Task<Images> GetNewImageIdAsync(string filename)
    {
        throw new NotImplementedException();
    }

    public async Task SaveNewImageAsync(
        string filename,
        Stream content,
        (int width, int height)? dimensions = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("Filename must be provided.", nameof(filename));
        }

        ArgumentNullException.ThrowIfNull(content);

        _logger.LogInformation("Saving new image {FileName}", filename);

        // 1. Save the image using the injected image storage
        await _imageStorage.SaveAsync(filename, content, cancellationToken).ConfigureAwait(false);

        // 2. Save image metadata to database
        _imageRepository.SaveImageDetailsAsync(filename, filename, "image/jpeg", content.Length, dimensions!.Value.width, dimensions.Value.height, "FileSystem", "system", cancellationToken).Wait(cancellationToken);
        _logger.LogInformation("Image {FileName} saved successfully", filename);
    }
}
