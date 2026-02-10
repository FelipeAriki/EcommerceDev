using EcommerceDev.Application.Commands.Orders.CreateOrder;
using EcommerceDev.Application.Common;
using EcommerceDev.Application.Queries.Orders.CalculateShipping;
using EcommerceDev.Application.Queries.Orders.GetAllOrders;
using EcommerceDev.Application.Queries.Orders.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDev.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var result = await _mediator.DispatchAsync<GetAllOrdersQuery, ResultViewModel<IEnumerable<GetAllOrdersItemViewModel>>>(new GetAllOrdersQuery());
        if (!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var result = await _mediator.DispatchAsync<GetOrderByIdQuery, ResultViewModel<GetOrderDetailsViewModel>>(new GetOrderByIdQuery(id));
        if (!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
    {
        var result = await _mediator
            .DispatchAsync<CreateOrderCommand, ResultViewModel<Guid>>(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("calculate-shipping")]
    public async Task<IActionResult> CalculateShipping(CalculateShippingQuery request)
    {
        var result = await _mediator
            .DispatchAsync<CalculateShippingQuery, ResultViewModel<decimal>>(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }
}
