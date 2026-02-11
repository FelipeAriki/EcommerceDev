using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.ProductCategories.GetProductCategoryById;

public class GetProductCategoryByIdQueryHandler : IHandler<GetProductCategoryByIdQuery, ResultViewModel<ProductCategoryViewModel>>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public GetProductCategoryByIdQueryHandler(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<ResultViewModel<ProductCategoryViewModel>> HandleAsync(GetProductCategoryByIdQuery request)
    {
        var category = await _productCategoryRepository.GetProductCategoryByIdAsync(request.Id);
        if (category is null) return ResultViewModel<ProductCategoryViewModel>.Error("Product category not found!");
        return ResultViewModel<ProductCategoryViewModel>.Success(new ProductCategoryViewModel
        {
            IdProductCategory = category.Id,
            Title = category.Title,
            Subcategory = category.Subcategory,
            Products = category.Products,
        });
    }
}
