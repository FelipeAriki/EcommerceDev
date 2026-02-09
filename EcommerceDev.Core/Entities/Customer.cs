namespace EcommerceDev.Core.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string Document { get; set; }
    public string? IdExternalPayment { get; set; }
    public IEnumerable<CustomerAddress> Addresses { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    public IEnumerable<OrderItemReview> Reviews { get; set; }

    protected Customer() { }
    public Customer(string fullName, string email, string phoneNumber, DateTime birthDate, string document)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Document = document;

        Addresses = [];
    }
}
