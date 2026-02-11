using EcommerceDev.Application.Commands.Customers.CreateCustomer;
using EcommerceDev.Application.Commands.Customers.CreateCustomerAddress;
using EcommerceDev.Application.Common;
using EcommerceDev.Application.Queries.Customers.GetAllCustomers;
using EcommerceDev.Application.Queries.Customers.GetCustomerById;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDev.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var result = await _mediator.DispatchAsync<GetAllCustomersQuery, ResultViewModel<IEnumerable<GetAllCustomersViewModel>>>(new GetAllCustomersQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var result = await _mediator.DispatchAsync<GetCustomerByIdQuery, ResultViewModel<GetAllCustomersViewModel>>(new GetCustomerByIdQuery(id));
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand request)
    {
        var result = await _mediator
            .DispatchAsync<CreateCustomerCommand, ResultViewModel<Guid>>(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("{customerId}/addresses")]
    public async Task<IActionResult> CreateAddress(CreateCustomerAddressCommand request)
    {
        var result = await _mediator
            .DispatchAsync<CreateCustomerAddressCommand, ResultViewModel<Guid>>(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }
}
