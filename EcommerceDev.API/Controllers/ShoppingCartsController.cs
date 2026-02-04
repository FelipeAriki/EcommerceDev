using EcommerceDev.Application.Commands.ShoppingCarts.CreateOrUpdateShoppingCart;
using EcommerceDev.Application.Common;
using EcommerceDev.Application.Common.ShoppingCarts;
using EcommerceDev.Application.Queries.ShoppingCarts.GetShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDev.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShoppingCartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShoppingCartsController(Guid id)
    {
        var query = new GetShoppingCartQuery(id);

        var result = await _mediator.DispatchAsync<GetShoppingCartQuery, ResultViewModel<IEnumerable<ProductItemShoppingCartModel>>>(query);

        if (!result.IsSuccess)
            return NotFound(result.Message);

        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CreateOrUpdateShoppingCart(Guid id, CreateOrUpdateShoppingCartCommand command)
    {
        command.IdCustomer = id;

        var result = await _mediator.DispatchAsync<CreateOrUpdateShoppingCartCommand, ResultViewModel<bool>>(command);

        if (!result.IsSuccess)
            return NotFound(result.Message);

        return Ok(result.Data);
    }
}
