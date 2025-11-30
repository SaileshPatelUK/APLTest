using APLTechnical.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APLTechnical.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous] // Should probably have roles permissions
[ApiController]
public class ImageController(IImageService imageService, ILogger<ImageController> log) : ControllerBase
{
    [HttpGet("GetNewImageId/{filename}")]
    public async Task<IActionResult> GetNewImageIdAsync(string filename)
    {
        try
        {
            log.LogInformation($"API GetNewImageIdAsync with ({filename})");
            var newImageId = await imageService.GetNewImageIdAsync(filename);
            log.LogInformation($"API GetNewImageIdAsync with ({filename}) returns {newImageId}");
            return Ok(newImageId);
        }
        catch (Exception ex)
        {
            log.LogError(ex, $"Error in GetNewImageIdAsync with ({filename})");
            throw;
        }
    }
}
