using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Enums;

namespace EcommerceDev.Application.Queries.Orders.GetOrderById;

public class GetOrderDetailsViewModel
{
    public Guid IdCustomer { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public OrderStatus Status { get; set; }
    public Guid DeliveryAddressId { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal TotalProductsPrice { get; set; }
    public string? IdExternalOrder { get; set; }
    public string? PaymentUrl { get; set; }

    public static GetOrderDetailsViewModel ToViewModel(Order order)
    {
        return new GetOrderDetailsViewModel
        {
            IdCustomer = order.IdCustomer,
            ConfirmationDate = order.ConfirmationDate,
            ShippingDate = order.ShippingDate,
            Status = order.Status,
            DeliveryAddressId = order.DeliveryAddressId,
            ShippingPrice = order.ShippingPrice,
            TotalProductsPrice = order.TotalProductsPrice,
            IdExternalOrder = order.IdExternalOrder,
            PaymentUrl = order.PaymentUrl,
        };
    }
}
