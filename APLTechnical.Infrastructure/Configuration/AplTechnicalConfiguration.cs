using APLTechnical.Core.Enums;

namespace APLTechnical.Infrastructure.Configuration;

public class AplTechnicalConfiguration
{
    public ImageStorageProvider ImageStorageProvider { get; set; } = 0;
    public string SqlConnectionString { get; set; } = string.Empty;

    public BlobStorageConfiguration BlobStorage { get; set; } = new BlobStorageConfiguration();
    public string ImageFileSystemRoot { get; set; } = string.Empty;
}
