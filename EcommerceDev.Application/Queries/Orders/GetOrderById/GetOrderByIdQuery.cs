namespace EcommerceDev.Application.Queries.Orders.GetOrderById;

public class GetOrderByIdQuery
{
    public Guid Id { get; set; }

    public GetOrderByIdQuery(Guid id)
    {
        Id = id;
    }
}
