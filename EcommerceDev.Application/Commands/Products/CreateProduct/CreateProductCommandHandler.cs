using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Caching;

namespace EcommerceDev.Application.Commands.Products.CreateProduct;

public class CreateProductCommandHandler
    : IHandler<CreateProductCommand, ResultViewModel<Guid>>
{
    private readonly IProductRepository _repository;
    private readonly ICacheService _cacheService;
    private const string _cacheKeyPrefix = "products:all";

    public CreateProductCommandHandler(IProductRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<ResultViewModel<Guid>> HandleAsync(CreateProductCommand request)
    {
        var product = new Product(request.Title, request.Description, request.Price, request.Brand, request.Quantity,
            request.IdCategory);

        await _repository.CreateProductAsync(product);

        await _cacheService.RemoveAsync(_cacheKeyPrefix);

        return ResultViewModel<Guid>.Success(product.Id);
    }
}