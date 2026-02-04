using EcommerceDev.Application.Common;
using EcommerceDev.Infrastructure.Storage;

namespace EcommerceDev.Application.Queries.Products.DownloadAllImagesForProduct;

public class DownloadAllImagesForProductQueryHandler : IHandler<DownloadAllImagesForProductQuery, ResultViewModel<IEnumerable<Stream>>>
{
    private readonly IBlobStorageService _storageService;

    public DownloadAllImagesForProductQueryHandler(
        IBlobStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<ResultViewModel<IEnumerable<Stream>>> HandleAsync(DownloadAllImagesForProductQuery request)
    {
        var streams = await _storageService.DownloadImages($"{request.IdProduct}/");

        return ResultViewModel<IEnumerable<Stream>>.Success(streams);
    }
}
