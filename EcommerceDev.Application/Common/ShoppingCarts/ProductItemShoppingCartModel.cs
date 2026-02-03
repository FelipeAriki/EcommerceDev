namespace EcommerceDev.Application.Common.ShoppingCarts;

public class ProductItemShoppingCartModel
{
    public Guid IdProduct { get; set; }
    public int Quantity { get; set; }

    public ProductItemShoppingCartModel(Guid idProduct, int quantity)
    {
        IdProduct = idProduct;
        Quantity = quantity;
    }
}