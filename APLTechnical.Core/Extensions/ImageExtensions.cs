namespace APLTechnical.Core.Extensions;

public static class ImageExtensions
{
    public static (int width, int height) ValidateMaxDimensions(
        this Stream stream,
        int maxWidth = 1024,
        int maxHeight = 1024)
    {
        ArgumentNullException.ThrowIfNull(stream);

        var originalPos = stream.Position;
        stream.Position = 0;

        using var img = System.Drawing.Image.FromStream(
            stream,
            useEmbeddedColorManagement: false,
            validateImageData: false);

        var width = img.Width;
        var height = img.Height;

        if (width > maxWidth || height > maxHeight)
        {
            throw new InvalidOperationException(
                $"Image dimensions too large ({width}x{height}). " +
                $"Maximum allowed is {maxWidth}x{maxHeight}.");
        }

        stream.Position = originalPos;

        return (width, height);
    }
}
