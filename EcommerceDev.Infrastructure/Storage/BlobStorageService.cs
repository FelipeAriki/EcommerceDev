
using Azure.Storage.Blobs;

namespace EcommerceDev.Infrastructure.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;
    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobContainerClient = blobServiceClient.GetBlobContainerClient("product-photos");
    }

    public async Task<bool> UploadImage(string path, Stream fileStream)
    {
        var blobClient = _blobContainerClient.GetBlobClient(path);

        var response = await blobClient.UploadAsync(fileStream);
        if (response == null) return false;
        return true;
    }

    public async Task<Stream> DownloadImage(string path)
    {
        var blobClient = _blobContainerClient.GetBlobClient(path);
        var stream = new MemoryStream();

        var response = await blobClient.DownloadToAsync(stream);

        stream.Position = 0;

        return response.IsError ? throw new Exception($"Error downloading image: {path}") : stream;
    }

    public async Task<List<Stream>> DownloadImages(string path)
    {
        var streams = new List<Stream>();

        await foreach (var item in _blobContainerClient.GetBlobsAsync(prefix: path))
        {
            var blobClient = _blobContainerClient.GetBlobClient(item.Name);

            var stream = new MemoryStream();

            var response = await blobClient.DownloadToAsync(stream);

            stream.Position = 0;

            streams.Add(stream);
        }

        return streams;
    }
}
