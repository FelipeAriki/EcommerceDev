namespace EcommerceDev.Application.Queries.Products.DownloadImageForProduct;

public class DownloadImageForProductQuery
{
    public Guid IdProductImage { get; set; }
    public DownloadImageForProductQuery(Guid idProductImage)
    {
        IdProductImage = idProductImage;
    }
}
