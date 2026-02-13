using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
    Task<ProductCategory?> GetProductCategoryByIdAsync(Guid id);
    Task<Guid> CreateProductCategoryAsync(ProductCategory productCategory);
    Task UpdateProductCategoryAsync(ProductCategory productCategory);
}