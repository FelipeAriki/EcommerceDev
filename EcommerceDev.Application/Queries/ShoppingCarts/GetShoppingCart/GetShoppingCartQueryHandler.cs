using EcommerceDev.Application.Common;
using EcommerceDev.Application.Common.ShoppingCarts;
using EcommerceDev.Infrastructure.Caching;

namespace EcommerceDev.Application.Queries.ShoppingCarts.GetShoppingCart;

public class GetShoppingCartQueryHandler : IHandler<GetShoppingCartQuery, ResultViewModel<IEnumerable<ProductItemShoppingCartModel>>>
{
    private readonly ICacheService _cacheService;

    public GetShoppingCartQueryHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<ResultViewModel<IEnumerable<ProductItemShoppingCartModel>>> HandleAsync(GetShoppingCartQuery request)
    {
        var cacheKey = request.IdCustomer.ToString();

        var cacheResult = await _cacheService.GetAsync<IEnumerable<ProductItemShoppingCartModel>>(cacheKey);

        if (cacheResult is null)
        {
            return ResultViewModel<IEnumerable<ProductItemShoppingCartModel>>.Error("Registro não encontrado.");
        }

        return ResultViewModel<IEnumerable<ProductItemShoppingCartModel>>.Success(cacheResult);
    }
}
