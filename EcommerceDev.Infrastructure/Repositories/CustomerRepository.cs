using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;

namespace EcommerceDev.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly EcommerceDbContext _context;
    public CustomerRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return customer.Id;
    }

    public async Task<Guid> CreateAddressAsync(CustomerAddress address)
    {
        await _context.CustomerAddresses.AddAsync(address);
        await _context.SaveChangesAsync();

        return address.Id;
    }
}
