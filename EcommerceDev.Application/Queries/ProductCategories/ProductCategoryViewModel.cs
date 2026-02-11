using EcommerceDev.Core.Entities;

namespace EcommerceDev.Application.Queries.ProductCategories;

public class ProductCategoryViewModel
{
    public Guid IdProductCategory { get; set; }
    public string Title { get; set; }
    public string Subcategory { get; set; }
    public IEnumerable<Product> Products { get; set; } = [];
}
