namespace APLTechnical.Core.Models;

public class Images
{
    public int Id { get; set; }

    public string OriginalFileName { get; set; } = null!;
    public string BlobNameOrPath { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long SizeBytes { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }

    // "Blob" or "Local"
    public string StorageLocation { get; set; } = null!;

    public DateTime UploadedAtUtc { get; set; }
    public string? UploadedBy { get; set; }
}
