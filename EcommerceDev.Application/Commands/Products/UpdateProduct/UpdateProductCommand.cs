namespace EcommerceDev.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommand
{
    public Guid IdProduct { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Brand { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
