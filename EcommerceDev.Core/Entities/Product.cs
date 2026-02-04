namespace EcommerceDev.Core.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public Guid IdCategory { get; set; }
    public ProductCategory Category { get; set; }
    public IEnumerable<OrderItemReview> Reviews { get; set; } = [];
    public IEnumerable<ProductImage> Images { get; set; } = [];

    protected Product() { }
    public Product(string title, string description, decimal price, string brand, int quantity, Guid idCategory)
    {
        Title = title;
        Description = description;
        Price = price;
        Brand = brand;
        Quantity = quantity;
        IdCategory = idCategory;
    }
}
