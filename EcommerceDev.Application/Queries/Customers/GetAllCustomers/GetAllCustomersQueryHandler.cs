using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.Customers.GetAllCustomers;

public class GetAllCustomersQueryHandler : IHandler<GetAllCustomersQuery, ResultViewModel<IEnumerable<GetAllCustomersViewModel>>>
{
    private readonly ICustomerRepository _customerRepository;
    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ResultViewModel<IEnumerable<GetAllCustomersViewModel>>> HandleAsync(GetAllCustomersQuery request)
    {
        var customers = await _customerRepository.GetCustomersAsync();
        if (customers is null) return ResultViewModel<IEnumerable<GetAllCustomersViewModel>>.Error("Customer not found!");
        return ResultViewModel<IEnumerable<GetAllCustomersViewModel>>.Success(customers.Select(c => new GetAllCustomersViewModel
        {
            IdCustomer = c.Id,
            FullName = c.FullName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            BirthDate = c.BirthDate,
            Document = c.Document,
            IdExternalPayment = c.IdExternalPayment
        }));
    }
}
