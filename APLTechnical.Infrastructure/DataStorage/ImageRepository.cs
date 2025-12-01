using APLTechnical.Core.Extensions;
using APLTechnical.Infrastructure.DataStorage.Context;
using APLTechnical.Infrastructure.DataStorage.Entities;
using APLTechnical.Infrastructure.DataStorage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLTechnical.Infrastructure.DataStorage;

public class ImageRepository(AplContext context) : IdentityRepositoryBase<ImageEntity>(context), IImageRepository
{
    private readonly AplContext _context = context;

    public async Task<string?> ImageExistsNameAsync(string filename)
    {
        var existingImage = await _context.Images
            .Where(oi => oi.OriginalFileName == filename)
            .OrderByDescending(oi => oi.Id)
            .Take(1)
            .FirstOrDefaultAsync();

        return existingImage == null ? null : $"{existingImage.Id}_{existingImage.OriginalFileName}";
    }

    public async Task<ImageEntity> SaveImageDetailsAsync(
           string originalFileName,
           string blobNameOrPath,
           string contentType,
           long sizeBytes,
           int width,
           int height,
           string storageLocation,
           string? uploadedBy,
           CancellationToken cancellationToken = default)
    {
        blobNameOrPath.ThrowIfNullOrWhiteSpace(nameof(originalFileName));
        blobNameOrPath.ThrowIfNullOrWhiteSpace(nameof(blobNameOrPath));
        blobNameOrPath.ThrowIfNullOrWhiteSpace(nameof(contentType));
        blobNameOrPath.ThrowIfNullOrWhiteSpace(nameof(storageLocation));

        var entity = new ImageEntity
        {
            OriginalFileName = originalFileName,
            BlobNameOrPath = blobNameOrPath,
            ContentType = contentType,
            SizeBytes = sizeBytes,
            Width = width,
            Height = height,
            StorageLocation = storageLocation,
            UploadedAtUtc = DateTime.UtcNow,
            UploadedBy = uploadedBy,
        };

        _context.Images.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
