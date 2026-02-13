namespace EcommerceDev.Core.Entities;

public class ProductCategory : BaseEntity
{
    public string Title { get; set; }
    public string Subcategory { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();


    protected ProductCategory() { }
    public ProductCategory(string title, string subcategory)
    {
        Title = title;
        Subcategory = subcategory;
        Products = new List<Product>();
    }
}
