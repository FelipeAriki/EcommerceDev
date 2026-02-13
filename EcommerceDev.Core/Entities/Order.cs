using EcommerceDev.Core.Enums;

namespace EcommerceDev.Core.Entities;

public class Order : BaseEntity
{
    public Guid IdCustomer { get; private set; }
    public Customer Customer { get; private set; }
    public DateTime? ConfirmationDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public OrderStatus Status { get; set; }
    public Guid DeliveryAddressId { get; private set; }
    public CustomerAddress DeliveryAddress { get; private set; }
    public decimal ShippingPrice { get; set; }
    public decimal TotalProductsPrice { get; set; }
    public string? IdExternalOrder { get; set; }
    public string? PaymentUrl { get; set; }
    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
    public ICollection<OrderUpdate> Updates { get; private set; } = new List<OrderUpdate>();


    protected Order() { }
    public Order(Guid idCustomer, Guid deliveryAddressId, decimal shippingPrice, IEnumerable<OrderItem> items)
    {
        IdCustomer = idCustomer;
        Status = OrderStatus.Created;
        DeliveryAddressId = deliveryAddressId;
        ShippingPrice = shippingPrice;
        Items = new List<OrderItem>(items);
        Updates = new List<OrderUpdate>();
    }

    public void MarkAsPaymentPending()
    {
        if (Status != OrderStatus.Created)
        {
            Console.WriteLine($"[Order] Order is in invalid state for payment.");

            return;
        }

        Status = OrderStatus.PaymentPending;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaymentExpired()
    {
        if (Status != OrderStatus.PaymentPending)
        {
            Console.WriteLine($"[Order] Order is in invalid state for payment expiration.");

            throw new Exception("Order is in invalid state for payment expiration.");
        }

        Status = OrderStatus.PaymentExpired;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetTotalProductsPrice(decimal totalProductsPrice)
    {
        TotalProductsPrice = totalProductsPrice;
    }
}
