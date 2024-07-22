using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    private readonly BlobStorageService _blobStorageService;

    public UploadController(BlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadBlobs(List<IFormFile> files)
    {
        var response = await _blobStorageService.UploadFiles(files);
        return Ok(response);
    }

    [HttpGet("blobs")]
    public async Task<IActionResult> GetAllBlobs()
    {
        var response = await _blobStorageService.GetUploadedBlobNamesAsync();
        return Ok(response);
    }
}
