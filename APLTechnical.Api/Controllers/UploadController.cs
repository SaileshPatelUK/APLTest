using APLTechnical.Core.Extensions;
using APLTechnical.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APLTechnical.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class UploadController(IImageService imageService, ILogger<UploadController> log) : ControllerBase
{
    private readonly IImageService _imageService = imageService;
    private readonly ILogger<UploadController> _log = log;

    [HttpPost]
    public async Task<IActionResult> UploadImageAsync(
        [FromForm] IFormFile file,
        CancellationToken cancellationToken)
    {
        // Basic validation
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Validate file type
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (extension is not ".png" and not ".jpg" and not ".jpeg")
        {
            return BadRequest("Only .png and .jpg image formats are allowed.");
        }

        try
        {
            _log.LogInformation("UploadImageAsync: received file {FileName} ({Length} bytes)", file.FileName, file.Length);

            using var stream = file.OpenReadStream();
            // Validate image dimensions
            stream.ValidateMaxDimensions();

            // Save the image using the image service
            await _imageService.SaveNewImageAsync(file.FileName, stream, cancellationToken);

            // Return something useful to the client
            return Ok(new
            {
                fileName = file.FileName,
                size = file.Length,
            });
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error in UploadImageAsync for file {FileName}", file.FileName);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image.");
        }
    }
}
