using APLTechnical.Core.Enums;

namespace APLTechnical.Infrastructure.Configuration;

public class AplTechnicalConfiguration
{
    public ImageStorageProvider ImageStorageProvider { get; set; } = 0;
    public required string SqlConnectionString { get; set; }

    public required BlobStorageConfiguration BlobStorage { get; set; }
}
