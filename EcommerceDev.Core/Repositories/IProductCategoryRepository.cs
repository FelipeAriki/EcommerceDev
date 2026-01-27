using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface IProductCategoryRepository
{
    Task<Guid> CreateProductCategoryAsync(ProductCategory productCategory);
}