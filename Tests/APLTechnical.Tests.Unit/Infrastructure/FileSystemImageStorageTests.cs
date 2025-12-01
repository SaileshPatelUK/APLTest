using System.Text;
using APLTechnical.Infrastructure.ImageStorage;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace APLTechnical.Tests.Unit.Infrastructure;

public class FileSystemImageStorageTests : IDisposable
{
    private readonly string _tempdir;
    private readonly FileSystemImageStorage _fsis;

    public FileSystemImageStorageTests()
    {
        _tempdir = Path.Combine(
            Path.GetTempPath(),
            "FileSystemImageStorageTests",
            Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(_tempdir);

        var config = Substitute.For<IConfiguration>();
        config["APLTechnical:ImageFileSystemRoot"].Returns(_tempdir);

        _fsis = new FileSystemImageStorage(config);
    }

    [Fact]
    public async Task SaveAsync_CreatesDirectoryAndWritesFile()
    {
        // Arrange
        var filename = Path.Combine("subfolder", "test-image.jpg");
        var fullPath = Path.Combine(_tempdir, filename);

        var contentText = "content";
        await using var contentStream =
        new MemoryStream(Encoding.UTF8.GetBytes(contentText));

        // Act
        await _fsis.SaveAsync(filename, contentStream);

        // Assert
        using (new AssertionScope())
        {
            File.Exists(fullPath).Should().BeTrue("the file should be created on disk");

            var written = await File.ReadAllTextAsync(fullPath);
            written.Should().Be(contentText, "the file content should match what was written");
        }
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempdir))
            {
                Directory.Delete(_tempdir, recursive: true);
            }
        }
        catch
        {
            // ignore cleanup errors
        }
    }
}
