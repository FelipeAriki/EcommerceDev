using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IHandler<UpdateCustomerCommand, ResultViewModel>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResultViewModel> HandleAsync(UpdateCustomerCommand request)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.IdCustomer);
            if (customer is null) return ResultViewModel.Error("Customer not found!");

            customer.FullName = request.FullName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.BirthDate = request.BirthDate;
            customer.Document = request.Document;

            await _customerRepository.UpdateCustomerAsync(customer);
            return ResultViewModel.Success();
        }
    }
}
