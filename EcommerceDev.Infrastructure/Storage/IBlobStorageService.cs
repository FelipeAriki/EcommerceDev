namespace EcommerceDev.Infrastructure.Storage;

public interface IBlobStorageService
{
    Task<bool> UploadImage(string path, Stream fileStream);
    Task<Stream> DownloadImage(string path);
    Task<List<Stream>> DownloadImages(string path);
}
