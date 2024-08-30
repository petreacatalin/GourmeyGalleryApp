using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly BlobStorageService _blobStorageService;
    private readonly IConfiguration _configuration;

    public UploadController(BlobStorageService blobStorageService ,IConfiguration configuration)
    {
        _blobStorageService = blobStorageService;
        _configuration = configuration;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadBlobs(IFormFile file)
    {        
     
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var blobStorageService = new BlobStorageService(_configuration);
        var imageUrl = await blobStorageService.UploadFile(file);

        return Content(imageUrl,"text/plain"); // Return the URL as plain tex
    }

    [HttpGet("blobs")]
    public async Task<IActionResult> GetAllBlobs()
    {
        var response = await _blobStorageService.GetUploadedBlobNamesAsync();
        return Ok(response);
    }
}
