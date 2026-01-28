using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<Guid> CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
}