using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface IProductRepository
{
    Task<Guid> CreateProductAsync(Product product);
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);

    Task CreateProductImageAsync(ProductImage productImage);
    Task<ProductImage?> GetProductImageById(Guid id);
}