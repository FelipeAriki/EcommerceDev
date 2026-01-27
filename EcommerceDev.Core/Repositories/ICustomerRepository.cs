using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface ICustomerRepository
{
    Task<Guid> CreateCustomerAsync(Customer customer);
    Task<Guid> CreateAddressAsync(CustomerAddress address);
}