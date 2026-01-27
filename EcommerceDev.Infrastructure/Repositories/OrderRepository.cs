using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Guid> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
