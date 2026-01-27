using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Guid> CreateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}
