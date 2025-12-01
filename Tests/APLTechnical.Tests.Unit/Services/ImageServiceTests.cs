using APLTechnical.Core.Interfaces;                     // IImageService
using APLTechnical.Infrastructure.DataStorage.Interfaces;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using APLTechnical.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace APLTechnical.Tests.Unit.Services;
public class ImageServiceTests
{
    private readonly IImageStorage _imageStorage;
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<ImageService> _logger;

    private readonly IImageService _imageService;

    public ImageServiceTests()
    {
        _imageStorage = Substitute.For<IImageStorage>();
        _imageRepository = Substitute.For<IImageRepository>();
        _logger = Substitute.For<ILogger<ImageService>>();

        _imageStorage
            .SaveAsync(Arg.Any<string>(), Arg.Any<Stream>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _imageRepository
            .SaveImageDetailsAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<long>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        _imageService = new ImageService(
            _imageStorage,
            _imageRepository,
            _logger);
    }

    [Fact]
    public async Task SaveNewImageAsync_ThrowsArgumentException_WhenFilenameIsNull()
    {
        // Arrange
        string? filename = null;
        using var content = new MemoryStream([1, 2, 3]);

        // Act
        Func<Task> act = () => _imageService.SaveNewImageAsync(filename!, content);

        // Assert
        using (new AssertionScope())
        {
            var ex = await act.Should().ThrowAsync<ArgumentException>();
            ex.Which.ParamName.Should().Be("filename");
            ex.Which.Message.Should().StartWith("Filename must be provided.");
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task SaveNewImageAsync_ThrowsArgumentException_WhenFilenameIsEmptyOrWhitespace(string filename)
    {
        using var content = new MemoryStream([1, 2, 3]);

        Func<Task> act = () => _imageService.SaveNewImageAsync(filename, content);

        using (new AssertionScope())
        {
            var ex = await act.Should().ThrowAsync<ArgumentException>();
            ex.Which.ParamName.Should().Be("filename");
        }
    }

    [Fact]
    public async Task SaveNewImageAsync_ThrowsArgumentNullException_WhenContentIsNull()
    {
        var filename = "test.jpg";
        Stream? content = null;

        Func<Task> act = () => _imageService.SaveNewImageAsync(filename, content!);

        using (new AssertionScope())
        {
            var ex = await act.Should().ThrowAsync<ArgumentNullException>();
            ex.Which.ParamName.Should().Be("content");
        }
    }

    [Fact]
    public async Task SaveNewImageAsync_CallsStorageAndRepository_WithCorrectArguments()
    {
        // Arrange
        var filename = "test.jpg";
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        using var content = new MemoryStream(bytes);

        var dims = (width: 800, height: 600);

        // Act
        await _imageService.SaveNewImageAsync(filename, content, dims);

        // Assert
        using (new AssertionScope())
        {
            // 1. IImageStorage was called correctly
            await _imageStorage
                .Received(1)
                .SaveAsync(
                    filename,
                    Arg.Is<Stream>(s => s == content && s.Length == bytes.Length),
                    Arg.Any<CancellationToken>());

            // 2. IImageRepository was called correctly
            await _imageRepository
                .Received(1)
                .SaveImageDetailsAsync(
                    filename,
                    filename,
                    "image/jpeg",
                    bytes.Length,
                    dims.width,
                    dims.height,
                    "FileSystem",
                    "system",
                    Arg.Any<CancellationToken>());
        }
    }
}
