using APLTechnical.Core.Models;

namespace APLTechnical.Core.Interfaces;

public interface IImageService
{
    Task<Images> GetNewImageIdAsync(string filename);
}
