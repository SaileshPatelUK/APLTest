namespace APLTechnical.Infrastructure.Configuration;

public class BlobStorageConfiguration
{
    public required string ConnectionString { get; set; }
    public required BlobContainerNames Containers { get; set; }
}

public class BlobContainerNames
{
    public string OriginalImages { get; set; } = "originalimages";
    public string ProcessedImages { get; set; } = "processedimages";
}
