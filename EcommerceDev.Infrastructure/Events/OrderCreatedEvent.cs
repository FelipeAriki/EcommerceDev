namespace EcommerceDev.Infrastructure.Events;

public class OrderCreatedEvent
{
    public Guid IdOrder { get; private set; }

    public OrderCreatedEvent(Guid idOrder)
    {
        IdOrder = idOrder;
    }
}
