using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Commands.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler : IHandler<UpdateCategoryCommand, ResultViewModel>
{
    private readonly IProductCategoryRepository _categoryRepository;
    public UpdateCategoryCommandHandler(IProductCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<ResultViewModel> HandleAsync(UpdateCategoryCommand request)
    {
        var category = await _categoryRepository.GetProductCategoryByIdAsync(request.IdCategory);
        if (category is null) return ResultViewModel.Error("Category not found!");
        category.Title = request.Title;
        category.Subcategory = request.Subcategory;
        await _categoryRepository.UpdateProductCategoryAsync(category);
        return ResultViewModel.Success();
    }
}
