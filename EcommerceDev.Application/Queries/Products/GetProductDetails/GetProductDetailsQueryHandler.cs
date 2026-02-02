using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Caching;

namespace EcommerceDev.Application.Queries.Products.GetProductDetails;

public class GetProductDetailsQueryHandler : IHandler<GetProductDetailsQuery, ResultViewModel<ProductDetailsViewModel>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICacheService _cacheService;
    private const string _cacheKeyPrefix = "product:";

    public GetProductDetailsQueryHandler(IProductRepository productRepository, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService;
    }

    public async Task<ResultViewModel<ProductDetailsViewModel>> HandleAsync(GetProductDetailsQuery request)
    {
        var cacheKey = $"{_cacheKeyPrefix}{request.IdProduct}";
        var cachedProduct = await _cacheService.GetAsync<ProductDetailsViewModel>(cacheKey);
        if (cachedProduct != null) return ResultViewModel<ProductDetailsViewModel>.Success(cachedProduct);

        var product = await _productRepository.GetProductByIdAsync(request.IdProduct);
        if (product == null) return ResultViewModel<ProductDetailsViewModel>.Error("Produto não encontrado!");

        var productDetailsViewModel = ProductDetailsViewModel.FromEntity(product);
        await _cacheService.SetAsync(cacheKey, productDetailsViewModel);
        return ResultViewModel<ProductDetailsViewModel>.Success(productDetailsViewModel);
    }
}