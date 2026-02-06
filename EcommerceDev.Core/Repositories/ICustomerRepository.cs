using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerById(Guid id);
    Task<Guid> CreateCustomerAsync(Customer customer);
    Task<Guid> CreateAddressAsync(CustomerAddress address);
    Task<CustomerAddress?> GetAddressAsync(Guid id);
}