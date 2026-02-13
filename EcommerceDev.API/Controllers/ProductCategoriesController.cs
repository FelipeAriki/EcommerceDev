using Azure.Core;
using EcommerceDev.Application.Commands.Categories.CreateCategory;
using EcommerceDev.Application.Commands.Categories.UpdateCategory;
using EcommerceDev.Application.Common;
using EcommerceDev.Application.Queries.ProductCategories;
using EcommerceDev.Application.Queries.ProductCategories.GetAllProductCategories;
using EcommerceDev.Application.Queries.ProductCategories.GetProductCategoryById;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDev.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mediator.DispatchAsync<GetProductCategoriesQuery, ResultViewModel<IEnumerable<ProductCategoryViewModel>>>(new GetProductCategoriesQuery());
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoriesById(Guid id)
    {
        var result = await _mediator.DispatchAsync<GetProductCategoryByIdQuery, ResultViewModel<ProductCategoryViewModel>>(new GetProductCategoryByIdQuery(id));
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand request)
    {
        var result = await _mediator
            .DispatchAsync<CreateCategoryCommand, ResultViewModel<Guid>>(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        var result = await _mediator.DispatchAsync<UpdateCategoryCommand, ResultViewModel>(command);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }
}
