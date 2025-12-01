using APLTechnical.Infrastructure.DataStorage.Entities;

namespace APLTechnical.Infrastructure.DataStorage.Interfaces;

public interface IImageRepository
{
    Task<string?> ImageExistsNameAsync(string filename);
    Task<ImageEntity> SaveImageDetailsAsync(
           string originalFileName,
           string blobNameOrPath,
           string contentType,
           long sizeBytes,
           int width,
           int height,
           string storageLocation,
           string? uploadedBy,
           CancellationToken cancellationToken = default);
}
