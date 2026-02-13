using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDev.Infrastructure.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly EcommerceDbContext _context;

    public ProductCategoryRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
    {
        return await _context.ProductCategories.AsNoTracking().ToListAsync();
    }

    public async Task<ProductCategory?> GetProductCategoryByIdAsync(Guid id)
    {
        return await _context.ProductCategories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Guid> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        await _context.ProductCategories.AddAsync(productCategory);
        await _context.SaveChangesAsync();

        return productCategory.Id;
    }

    public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
    {
        _context.ProductCategories.Update(productCategory);
        await _context.SaveChangesAsync();
    }
}
