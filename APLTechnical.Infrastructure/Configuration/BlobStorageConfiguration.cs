namespace APLTechnical.Infrastructure.Configuration;

public class BlobStorageConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ContainerName { get; set; } = "images";
}
