using APLTechnical.Core.Models;

namespace APLTechnical.Core.Interfaces;

public interface IImageService
{
    Task<Images> GetNewImageIdAsync(string filename);
    Task SaveNewImageAsync(
        string filename,
        Stream content,
        (int width, int height)? dimensions = null,
        CancellationToken cancellationToken = default);
}
