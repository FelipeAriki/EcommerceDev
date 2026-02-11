using EcommerceDev.Application.Common;
using EcommerceDev.Application.Queries.Customers.GetAllCustomers;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler : IHandler<GetCustomerByIdQuery, ResultViewModel<GetAllCustomersViewModel>>
{
    private readonly ICustomerRepository _customerRepository;
    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ResultViewModel<GetAllCustomersViewModel>> HandleAsync(GetCustomerByIdQuery request)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
        if (customer is null) return ResultViewModel<GetAllCustomersViewModel>.Error("Customer not found!");
        return ResultViewModel<GetAllCustomersViewModel>.Success(new GetAllCustomersViewModel
        {
            IdCustomer = customer.Id,
            FullName = customer.FullName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            BirthDate = customer.BirthDate,
            Document = customer.Document,
            IdExternalPayment = customer.IdExternalPayment
        });
    }
}
