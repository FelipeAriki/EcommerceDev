using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDev.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly EcommerceDbContext _context;
    public CustomerRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerById(Guid id)
    {
        return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return customer.Id;
    }

    public async Task<CustomerAddress?> GetAddressAsync(Guid id)
    {
        return await _context.CustomerAddresses.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> CreateAddressAsync(CustomerAddress address)
    {
        await _context.CustomerAddresses.AddAsync(address);
        await _context.SaveChangesAsync();

        return address.Id;
    }
}
