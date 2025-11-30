using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace APLTechnical.ImageProcessor;

public class ImageProcessor(ILogger<ImageProcessor> logger, BlobClient blobClient)
{
    private readonly ILogger<ImageProcessor> _logger = logger;
    private readonly BlobClient _blobClient = blobClient;

    [Function("ImageProcessor")]
    public async Task Run(
        [BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")] Stream blob,
        string name)
    {
        using var memoryStream = new MemoryStream();
        await blob.CopyToAsync(memoryStream).ConfigureAwait(false);
        var size = memoryStream.Length;

        _logger.LogInformation("C# Blob trigger function processed a blob. Name: {Name}, Size: {Size} bytes", name, size);

        // TODO: Add image processing logic here (resize, format conversion, upload, etc.)
        await _blobClient.UploadAsync(memoryStream);
    }
}
