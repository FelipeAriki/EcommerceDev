using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Storage;

namespace EcommerceDev.Application.Commands.Products.UploadImageForProduct;

public class UploadImageForProductCommandHandler : IHandler<UploadImageForProductCommand, ResultViewModel<bool>>
{
    private readonly IBlobStorageService _storageService;
    private readonly IProductRepository _productRepository;
    public UploadImageForProductCommandHandler(IBlobStorageService storageService, IProductRepository productRepository)
    {
        _storageService = storageService;
        _productRepository = productRepository;
    }

    public async Task<ResultViewModel<bool>> HandleAsync(UploadImageForProductCommand request)
    {
        var extensionFile = request.FileName.Split(".").Last();
        var productImage = new ProductImage(true, request.IdProduct);
        productImage.ConfigureIdentifier(extensionFile);

        await _storageService.UploadImage(productImage.Path, request.ImageStream);
        await _productRepository.CreateProductImageAsync(productImage);

        return ResultViewModel<bool>.Success(true);
    }
}
