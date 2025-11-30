namespace APLTechnical.Infrastructure.DataStorage.Interfaces;

public interface IImageRepository
{
    Task<string?> ImageExistsNameAsync(string filename);
}
