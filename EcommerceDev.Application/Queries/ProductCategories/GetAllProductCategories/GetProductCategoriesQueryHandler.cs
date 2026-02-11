using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.ProductCategories.GetAllProductCategories;

public class GetProductCategoriesQueryHandler : IHandler<GetProductCategoriesQuery, ResultViewModel<IEnumerable<ProductCategoryViewModel>>>
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public GetProductCategoriesQueryHandler(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }
    public async Task<ResultViewModel<IEnumerable<ProductCategoryViewModel>>> HandleAsync(GetProductCategoriesQuery request)
    {
        var categories = await _productCategoryRepository.GetProductCategoriesAsync();
        if (categories is null) return ResultViewModel<IEnumerable<ProductCategoryViewModel>>.Error("Product category not found!");
        return ResultViewModel<IEnumerable<ProductCategoryViewModel>>.Success(categories.Select(c => new ProductCategoryViewModel
        {
            IdProductCategory = c.Id,
            Title = c.Title,
            Subcategory = c.Subcategory,
            Products = c.Products,
        }).ToList());
    }
}
