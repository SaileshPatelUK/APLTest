using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Infrastructure.ImageStorage;

public class FileSystemImageStorage(IConfiguration config) : IImageStorage
{
    private readonly string _rootPath = config["APLTechnical:ImageFileSystemRoot"] ?? "c:\\images";

    public async Task SaveAsync(string path, string? blobName, Stream content, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_rootPath, path);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        await using var file = File.Create(fullPath);
        await content.CopyToAsync(file, cancellationToken);
    }

    public Task<Stream> GetAsync(string path, string? blobName, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_rootPath, path);
        Stream stream = File.OpenRead(fullPath);
        return Task.FromResult(stream);
    }
}
