using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface IOrderRepository
{
    Task<Guid> CreateOrderAsync(Order order);
}