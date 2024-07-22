using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(string connectionString, string containerName)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
        _containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<List<Uri>> UploadFiles(List<IFormFile> files)
    {
        var blobUris = new List<Uri>();

        foreach (var file in files)
        {
            string fileName = file.FileName;
            using (var stream = file.OpenReadStream())
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(stream, true);
                blobUris.Add(blobClient.Uri);
            }
        }

        return blobUris;
    }

    public async Task<List<string>> GetUploadedBlobNamesAsync()
    {
        var blobNames = new List<string>();
        await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
        {
            blobNames.Add(blobItem.Name);
        }
        return blobNames;
    }
}
