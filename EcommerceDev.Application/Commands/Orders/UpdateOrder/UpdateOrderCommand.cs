using EcommerceDev.Core.Enums;

namespace EcommerceDev.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommand
{
    public Guid IdOrder { get; set; }
    public DateTime ConfirmationDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal TotalProductsPrice { get; set; }
}
