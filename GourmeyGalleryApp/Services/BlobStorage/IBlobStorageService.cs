public interface IBlobStorageService
{
    Task<string> UploadFile(IFormFile file);
    Task<List<string>> GetUploadedBlobNamesAsync();
}