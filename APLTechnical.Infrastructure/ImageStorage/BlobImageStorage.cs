using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace APLTechnical.Infrastructure.ImageStorage;

public class BlobImageStorage(ILogger<BlobImageStorage> logger, BlobServiceClient client) : IImageStorage
{
    private readonly ILogger<BlobImageStorage> _logger = logger;
    private readonly BlobServiceClient _client = client;
    private readonly string _containerName = "images";

    public async Task SaveAsync(
        string filename,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        var container = _client.GetBlobContainerClient(_containerName);
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blob = container.GetBlobClient(filename);

        await blob.UploadAsync(
            content,
            overwrite: true,
            cancellationToken: cancellationToken);

        _logger.LogInformation("UploadImageAsync: File Saved Successfully");
    }

    public async Task<Stream> GetAsync(
        string filename,
        CancellationToken cancellationToken = default)
    {
        var container = _client.GetBlobContainerClient(_containerName);
        var blob = container.GetBlobClient(filename);

        var response = await blob.DownloadAsync(cancellationToken);
        return response.Value.Content;
    }
}
