namespace EcommerceDev.Core.Entities;

public class OrderUpdate : BaseEntity
{
    public string Description { get; set; }
    public Guid IdOrder { get; set; }

    protected OrderUpdate() { }
    public OrderUpdate(string description, Guid idOrder)
    {
        Description = description;
        IdOrder = idOrder;
    }
}
