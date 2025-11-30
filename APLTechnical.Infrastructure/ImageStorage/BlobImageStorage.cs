using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Azure.Storage.Blobs;

namespace APLTechnical.Infrastructure.ImageStorage;

public class BlobImageStorage(BlobServiceClient client) : IImageStorage
{
    public async Task SaveAsync(
        string path,
        string? blobName,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        // blobName = container name
        var container = client.GetBlobContainerClient(blobName);
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        // path = blob path inside container
        var blob = container.GetBlobClient(path);

        await blob.UploadAsync(
            content,
            overwrite: true,
            cancellationToken: cancellationToken);
    }

    public async Task<Stream> GetAsync(
        string path,
        string? blobName,
        CancellationToken cancellationToken = default)
    {
        var container = client.GetBlobContainerClient(blobName);
        var blob = container.GetBlobClient(path);

        var response = await blob.DownloadAsync(cancellationToken);
        return response.Value.Content;
    }
}
