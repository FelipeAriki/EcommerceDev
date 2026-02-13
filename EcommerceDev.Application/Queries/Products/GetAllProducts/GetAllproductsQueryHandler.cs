using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Caching;

namespace EcommerceDev.Application.Queries.Products.GetAllProducts;

public class GetAllproductsQueryHandler : IHandler<GetAllProductsQuery, ResultViewModel<IEnumerable<GetAllProductsItemViewModel>>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICacheService _cacheService;
    private const string _cacheKeyPrefix = "products:all";
    public GetAllproductsQueryHandler(IProductRepository productRepository, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _cacheService = cacheService;
    }

    public async Task<ResultViewModel<IEnumerable<GetAllProductsItemViewModel>>> HandleAsync(GetAllProductsQuery request)
    {
        var cachedProducts = await _cacheService.GetAsync<IEnumerable<GetAllProductsItemViewModel>>(_cacheKeyPrefix);

        if (cachedProducts != null)
        {
            return ResultViewModel<IEnumerable<GetAllProductsItemViewModel>>.Success(cachedProducts);
        }

        var products = await _productRepository.GetProductsAsync();

        var productsViewModel = products.Select(p => new GetAllProductsItemViewModel()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
        }).ToList();

        await _cacheService.SetAsync(_cacheKeyPrefix, productsViewModel);

        return ResultViewModel<IEnumerable<GetAllProductsItemViewModel>>.Success(productsViewModel);
    }
}