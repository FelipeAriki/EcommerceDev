using EcommerceDev.Application.Common.ShoppingCarts;

namespace EcommerceDev.Application.Commands.ShoppingCarts.CreateOrUpdateShoppingCart;

public class CreateOrUpdateShoppingCartCommand
{
    public Guid IdCustomer { get; set; }
    public List<ProductItemShoppingCartModel> Items { get; set; }

    public CreateOrUpdateShoppingCartCommand(Guid idCustomer, List<ProductItemShoppingCartModel> items)
    {
        IdCustomer = idCustomer;
        Items = items;
    }
}