using APLTechnical.Core.Enums;
using APLTechnical.Core.Interfaces;
using APLTechnical.Core.Models;
using APLTechnical.Infrastructure.ImageStorage.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APLTechnical.Services;

public class ImageService : IImageService
{
    private readonly Func<ImageStorageProvider, IImageStorage> _imageStorageFactory;
    private readonly ImageStorageProvider _provider;

    public ImageService(
        Func<ImageStorageProvider, IImageStorage> imageStorageFactory,
        IConfiguration config)
    {
        _imageStorageFactory = imageStorageFactory;

        // e.g. in appsettings.json: "APLTechnical:ImageStorageProvider": "Blob"
        var providerName = config["APLTechnical:ImageStorageProvider"] ?? "Blob";
        _provider = Enum.Parse<ImageStorageProvider>(providerName, ignoreCase: true);
    }

    public Task<Images> GetNewImageIdAsync(string filename)
    {
        throw new NotImplementedException();
    }
}
