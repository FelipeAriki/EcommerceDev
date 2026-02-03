namespace EcommerceDev.Application.Queries.ShoppingCarts.GetShoppingCart;

public class GetShoppingCartQuery
{
    public Guid IdCustomer { get; set; }

    public GetShoppingCartQuery(Guid idCustomer)
    {
        IdCustomer = idCustomer;
    }
}