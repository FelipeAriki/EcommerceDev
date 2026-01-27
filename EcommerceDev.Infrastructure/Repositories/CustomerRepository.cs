using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<Guid> CreateAddressAsync(CustomerAddress address)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
