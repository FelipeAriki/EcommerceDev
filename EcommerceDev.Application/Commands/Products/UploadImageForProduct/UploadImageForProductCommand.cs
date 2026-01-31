namespace EcommerceDev.Application.Commands.Products.UploadImageForProduct;

public class UploadImageForProductCommand
{
    public Guid IdProduct { get; set; }
    public string FileName { get; set; }
    public MemoryStream ImageStream { get; set; }

    public UploadImageForProductCommand(Guid idProduct, string fileName, MemoryStream imageStream)
    {
        IdProduct = idProduct;
        FileName = fileName;
        ImageStream = imageStream;
    }
}
