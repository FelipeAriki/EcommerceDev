using EcommerceDev.Core.Entities;

namespace EcommerceDev.Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommand
{
    public Guid IdCustomer { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Document { get; set; } = string.Empty;
}
