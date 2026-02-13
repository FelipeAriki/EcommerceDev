namespace EcommerceDev.Application.Commands.Categories.UpdateCategory;

public class UpdateCategoryCommand
{
    public Guid IdCategory { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subcategory { get; set; } = string.Empty;
}
