using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class BlobStorageService 
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
        _blobServiceClient = new BlobServiceClient(connectionString);
    }


    public async Task<string> UploadFile(IFormFile file)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(file.FileName);
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }

        return blobClient.Uri.ToString();
    }
    public async Task<List<string>> UploadFiles(List<IFormFile> files)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync();

        var urls = new List<string>();

        foreach (var file in files)
        {
            var blobClient = containerClient.GetBlobClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            urls.Add(blobClient.Uri.ToString());
        }

        return urls;
    }

    public async Task<List<string>> GetUploadedBlobNamesAsync()
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobUrls = new List<string>();

        await foreach (var blobItem in containerClient.GetBlobsAsync())
        {
            blobUrls.Add(containerClient.GetBlobClient(blobItem.Name).Uri.ToString());
        }

        return blobUrls;
    }
}
