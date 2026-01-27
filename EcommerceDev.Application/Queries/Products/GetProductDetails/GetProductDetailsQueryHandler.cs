using EcommerceDev.Application.Common;

namespace EcommerceDev.Application.Queries.Products.GetProductDetails;

public class GetProductDetailsQueryHandler
    : IHandler<GetProductDetailsQuery, ResultViewModel<ProductDetailsViewModel>>
{
    public Task<ResultViewModel<ProductDetailsViewModel>> HandleAsync(GetProductDetailsQuery request)
    {
        throw new NotImplementedException();
    }
}