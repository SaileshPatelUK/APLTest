using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Infrastructure.ImageStorage;

public class FileSystemImageStorage(IConfiguration config) : IImageStorage
{
    private readonly string _rootPath = config["APLTechnical:ImageFileSystemRoot"] ?? "c:\\images";

    public async Task SaveAsync(string filename, Stream content, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_rootPath, filename);
        var dir = Path.GetDirectoryName(fullPath)!;

        // Ensure directory exists
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        await using var file = File.Create(fullPath);
        await content.CopyToAsync(file, cancellationToken);
    }

    public Task<Stream> GetAsync(string filename, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_rootPath, filename);

        //  check if file exists (recommended)
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"File not found: {fullPath}");
        }

        Stream stream = File.OpenRead(fullPath);
        return Task.FromResult(stream);
    }
}
