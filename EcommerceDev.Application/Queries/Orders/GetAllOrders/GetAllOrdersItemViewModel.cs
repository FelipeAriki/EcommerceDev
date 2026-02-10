using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Enums;

namespace EcommerceDev.Application.Queries.Orders.GetAllOrders;

public class GetAllOrdersItemViewModel
{
    public Guid IdOrder { get; set; }
    public Guid IdCustomer { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public OrderStatus Status { get; set; }
    public Guid DeliveryAddressId { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal TotalProductsPrice { get; set; }
    public string? IdExternalOrder { get; set; }
    public string? PaymentUrl { get; set; }
}
