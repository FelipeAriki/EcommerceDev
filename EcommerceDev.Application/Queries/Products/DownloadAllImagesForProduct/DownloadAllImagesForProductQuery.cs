namespace EcommerceDev.Application.Queries.Products.DownloadAllImagesForProduct;

public class DownloadAllImagesForProductQuery
{
    public Guid IdProduct { get; set; }

    public DownloadAllImagesForProductQuery(Guid idProduct)
    {
        IdProduct = idProduct;
    }
}
