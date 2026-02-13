namespace EcommerceDev.Core.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string Document { get; set; }
    public string? IdExternalPayment { get; set; }
    public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<OrderItemReview> Reviews { get; set; } = new List<OrderItemReview>();


    protected Customer() { }
    public Customer(string fullName, string email, string phoneNumber, DateTime birthDate, string document)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Document = document;

        Addresses = new List<CustomerAddress>();
        Orders = new List<Order>();
        Reviews = new List<OrderItemReview>();
    }
}
