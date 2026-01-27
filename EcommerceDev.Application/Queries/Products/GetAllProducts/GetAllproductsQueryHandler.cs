using EcommerceDev.Application.Common;

namespace EcommerceDev.Application.Queries.Products.GetAllProducts;

public class GetAllproductsQueryHandler
    : IHandler<GetAllProductsQuery, ResultViewModel<List<GetAllProductsItemViewModel>>>
{
    public Task<ResultViewModel<List<GetAllProductsItemViewModel>>> HandleAsync(GetAllProductsQuery request)
    {
        throw new NotImplementedException();
    }
}