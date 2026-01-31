namespace EcommerceDev.Core.Entities;

public class ProductImage : BaseEntity
{
    public string Identifier { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public Guid IdProduct { get; set; }

    protected ProductImage() { }
    public ProductImage(bool isVisible, Guid idProduct)
    {
        IsVisible = isVisible;
        IdProduct = idProduct;
    }

    public void ConfigureIdentifier(string extensionFile)
    {
        Identifier = Id.ToString();
        Path = $"{IdProduct}/{Id}.{extensionFile}";
    }
}
