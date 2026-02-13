using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler : IHandler<UpdateProductCommand, ResultViewModel>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ResultViewModel> HandleAsync(UpdateProductCommand request)
    {
        var product = await _productRepository.GetProductByIdAsync(request.IdProduct);
        if (product is null) return ResultViewModel.Error("Product not found!");

        product.Title = request.Title;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Brand = request.Brand;
        product.Quantity = request.Quantity;

        await _productRepository.UpdateProductAsync(product);
        return ResultViewModel.Success();
    }
}
