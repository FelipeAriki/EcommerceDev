using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Commands.Products.CreateProduct;

public class CreateProductCommandHandler
    : IHandler<CreateProductCommand, ResultViewModel<Guid>>
{
    private readonly IProductRepository _repository;
    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<Guid>> HandleAsync(CreateProductCommand request)
    {
        var product = new Product(request.Title, request.Description, request.Price, request.Brand, request.Quantity,
            request.IdCategory);

        await _repository.CreateProductAsync(product);

        return ResultViewModel<Guid>.Success(product.Id);
    }
}