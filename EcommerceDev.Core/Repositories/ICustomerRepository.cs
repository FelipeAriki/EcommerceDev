using EcommerceDev.Core.Entities;

namespace EcommerceDev.Core.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerById(Guid id);
    Task<Guid> CreateCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task<Guid> CreateAddressAsync(CustomerAddress address);
    Task<CustomerAddress?> GetAddressAsync(Guid id);
}