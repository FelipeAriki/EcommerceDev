using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDev.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceDbContext _context;

    public OrderRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
            .SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Guid> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
