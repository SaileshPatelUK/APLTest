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
}
