using EcommerceDev.Application.Common;
using EcommerceDev.Infrastructure.Storage;

namespace EcommerceDev.Application.Queries.Products.DownloadAllImagesForProduct;

public class DownloadAllImagesForProductQueryHandler : IHandler<DownloadAllImagesForProductQuery, ResultViewModel<List<Stream>>>
{
    private readonly IBlobStorageService _storageService;

    public DownloadAllImagesForProductQueryHandler(
        IBlobStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<ResultViewModel<List<Stream>>> HandleAsync(DownloadAllImagesForProductQuery request)
    {
        var streams = await _storageService.DownloadImages($"{request.IdProduct}/");

        return ResultViewModel<List<Stream>>.Success(streams);
    }
}
