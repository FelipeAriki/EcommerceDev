using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Core.Services;

public class OrderDomainService : IOrderDomainService
{
    private const decimal PricePerKm = 30;
    private const decimal PricePerUnit = 2.5m;
    private const int MaximumAllowedDistanceKm = 250;
    private readonly IProductRepository _productRepository;

    public OrderDomainService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<decimal> CalculateProductOrderTotal(IEnumerable<OrderItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            total += item.Price * item.Quantity;
        }

        return total;
    }

    public decimal CalculateShippingCost(int distanceInKm, IEnumerable<OrderItem> items)
    {
        if (!items.Any()) throw new InvalidOperationException("No items found.");
        if (distanceInKm > MaximumAllowedDistanceKm || distanceInKm < 0)
            throw new ArgumentOutOfRangeException(nameof(distanceInKm), distanceInKm, "Distance out of range.");
        
        var totalPriceKm = distanceInKm == 0 ? PricePerKm : PricePerKm * distanceInKm;

        var totalUnits = items.Sum(i => i.Quantity);
        var totalPriceUnits = PricePerUnit * totalUnits;

        return totalPriceUnits + totalPriceKm;
    }

    public async Task UpdateProductPrices(Order order)
    {
        foreach (var item in order.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(item.IdProduct) ?? throw new InvalidOperationException("Product not found.");
            item.Price = product.Price;
        }
    }
}
