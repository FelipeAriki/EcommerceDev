namespace EcommerceDev.Core.Entities;

public class ProductCategory : BaseEntity
{
    public string Title { get; set; }
    public string Subcategory { get; set; }
    public IEnumerable<Product> Products { get; set; } = [];

    protected ProductCategory() { }
    public ProductCategory(string title, string subcategory)
    {
        Title = title;
        Subcategory = subcategory;
    }
}
