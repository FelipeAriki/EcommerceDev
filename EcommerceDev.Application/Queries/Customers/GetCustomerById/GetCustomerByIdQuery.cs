namespace EcommerceDev.Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQuery
{
    public Guid Id { get; set; }

    public GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }
}
