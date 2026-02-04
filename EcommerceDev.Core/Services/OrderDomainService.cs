using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Services;

public class OrderDomainService : IOrderDomainService
{
    private const decimal PricePerKm = 30;
    private const decimal PricePerUnit = 2.5m;

    public decimal CalculateShippingCost(int distanceInKm, IEnumerable<OrderItem> items)
    {
        var totalPriceKm = PricePerKm * distanceInKm;

        var totalUnits = items.Sum(i => i.Quantity);
        var totalPriceUnits = PricePerUnit * totalUnits;

        return totalPriceUnits + totalPriceKm;
    }
}
