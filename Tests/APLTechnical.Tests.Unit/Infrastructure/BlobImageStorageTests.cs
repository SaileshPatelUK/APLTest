using System.Text;
using APLTechnical.Infrastructure.ImageStorage;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace APLTechnical.Tests.Unit.Infrastructure;

public class BlobImageStorageTests
{
    private const string ContainerName = "images";

    [Fact]
    public async Task SaveAsync_UsesCorrectContainerAndFileName_AndCallsUpload()
    {
        // Arrange
        var logger = Substitute.For<ILogger<BlobImageStorage>>();
        var blobServiceClient = Substitute.For<BlobServiceClient>();
        var containerClient = Substitute.For<BlobContainerClient>();
        var blobClient = Substitute.For<BlobClient>();

        var filename = "test-image.png";
        await using var stream = new MemoryStream(Encoding.UTF8.GetBytes("dummy"));

        // Wiring: BlobServiceClient -> BlobContainerClient -> BlobClient
        blobServiceClient
            .GetBlobContainerClient(ContainerName)
            .Returns(containerClient);

        containerClient
            .GetBlobClient(filename)
            .Returns(blobClient);

        var sut = new BlobImageStorage(logger, blobServiceClient);

        // Act
        await sut.SaveAsync(filename, stream, CancellationToken.None);

        // Assert
        using var scope = new AssertionScope();
        blobServiceClient.Received(1).GetBlobContainerClient(ContainerName);

        containerClient.Received(1).GetBlobClient(filename);

        await blobClient.ReceivedWithAnyArgs(1).UploadAsync(
            Arg.Any<Stream>(),
            overwrite: true,
            cancellationToken: Arg.Any<CancellationToken>());

        logger.ReceivedWithAnyArgs().LogInformation("UploadImageAsync: File Saved Successfully");
    }

    [Fact]
    public async Task GetAsync_DownloadsBlobAndReturnsContentStream()
    {
        // Arrange
        var logger = Substitute.For<ILogger<BlobImageStorage>>();
        var blobServiceClient = Substitute.For<BlobServiceClient>();
        var containerClient = Substitute.For<BlobContainerClient>();
        var blobClient = Substitute.For<BlobClient>();

        var filename = "test-image.jpg";
        var originalContent = "some-image-content";
        var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(originalContent));

        // Create a BlobDownloadInfo with our stream
        var downloadInfo = BlobsModelFactory.BlobDownloadInfo(
        content: contentStream);

        // Mock Response<BlobDownloadInfo>
        var response = Substitute.For<Response<BlobDownloadInfo>>();
        response.Value.Returns(downloadInfo);

        // Wiring
        blobServiceClient
            .GetBlobContainerClient(ContainerName)
            .Returns(containerClient);

        containerClient
            .GetBlobClient(filename)
            .Returns(blobClient);

        blobClient
            .DownloadAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(response));

        var sut = new BlobImageStorage(logger, blobServiceClient);

        // Act
        using var scope = new AssertionScope();
        using var resultStream = await sut.GetAsync(filename, CancellationToken.None);
        using var reader = new StreamReader(resultStream);
        var resultText = await reader.ReadToEndAsync();

        // Assert
        blobServiceClient.Received(1).GetBlobContainerClient(ContainerName);
        containerClient.Received(1).GetBlobClient(filename);
        await blobClient.Received(1).DownloadAsync(Arg.Any<CancellationToken>());

        Assert.Equal(originalContent, resultText);
    }
}
