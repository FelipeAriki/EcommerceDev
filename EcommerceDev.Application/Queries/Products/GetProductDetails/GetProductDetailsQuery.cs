namespace EcommerceDev.Application.Queries.Products.GetProductDetails;

public class GetProductDetailsQuery
{
    public Guid IdProduct { get; set; }

    public GetProductDetailsQuery(Guid idProduct)
    {
        IdProduct = idProduct;
    }
}