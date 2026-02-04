namespace EcommerceDev.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommand
{
    public Guid IdCustomer { get; set; }
    public IEnumerable<CreateOrderCommandItem> Items { get; set; } = [];
    public Guid DeliveryAddressId { get; set; }
}