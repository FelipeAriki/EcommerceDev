using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;

namespace EcommerceDev.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceDbContext _context;

    public OrderRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order.Id;
    }
}
