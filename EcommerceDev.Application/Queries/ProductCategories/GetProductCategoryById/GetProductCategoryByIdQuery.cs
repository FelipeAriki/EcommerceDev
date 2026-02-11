namespace EcommerceDev.Application.Queries.ProductCategories.GetProductCategoryById;

public class GetProductCategoryByIdQuery
{
    public Guid Id { get; set; }

    public GetProductCategoryByIdQuery(Guid id)
    {
        Id = id;
    }
}
