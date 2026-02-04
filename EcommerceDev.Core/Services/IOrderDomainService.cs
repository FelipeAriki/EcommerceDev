using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Services;

public interface IOrderDomainService
{
    decimal CalculateShippingCost(int distanceInKm, IEnumerable<OrderItem> items);
}
