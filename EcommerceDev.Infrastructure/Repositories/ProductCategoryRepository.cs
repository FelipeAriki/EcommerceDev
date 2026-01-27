using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Infrastructure.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    public Task<Guid> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        throw new NotImplementedException();
    }
}
