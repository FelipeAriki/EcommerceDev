using Azure.Core;
using EcommerceDev.Application.Commands.Products.CreateProduct;
using EcommerceDev.Application.Commands.Products.UpdateProduct;
using EcommerceDev.Application.Commands.Products.UploadImageForProduct;
using EcommerceDev.Application.Common;
using EcommerceDev.Application.Queries.Products.DownloadAllImagesForProduct;
using EcommerceDev.Application.Queries.Products.DownloadImageForProduct;
using EcommerceDev.Application.Queries.Products.GetAllProducts;
using EcommerceDev.Application.Queries.Products.GetProductDetails;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace EcommerceDev.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductDetails(Guid id)
    {
        var response = await _mediator.DispatchAsync<GetProductDetailsQuery, ResultViewModel<ProductDetailsViewModel>>(new GetProductDetailsQuery(id));
        if (!response.IsSuccess) return BadRequest(response);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _mediator.DispatchAsync<GetAllProductsQuery, ResultViewModel<IEnumerable<GetAllProductsItemViewModel>>>(new GetAllProductsQuery());
        if (!response.IsSuccess) return BadRequest(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand request)
    {
        var result = await _mediator
            .DispatchAsync<CreateProductCommand, ResultViewModel<Guid>>(request);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
    {
        var result = await _mediator
            .DispatchAsync<UpdateProductCommand, ResultViewModel>(command);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("{id:guid}/images")]
    public async Task<IActionResult> UploadProductPhoto(Guid id, IFormFile formFile)
    {
        var stream = new MemoryStream();
        await formFile.CopyToAsync(stream);
        stream.Position = 0;

        var command = new UploadImageForProductCommand(id, formFile.FileName, stream);
        var response = await _mediator.DispatchAsync<UploadImageForProductCommand, ResultViewModel<bool>>(command);
        if (!response.IsSuccess) return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("{id:guid}/images/{imageId:guid}")]
    public async Task<IActionResult> DownloadPhoto(Guid id, Guid imageId)
    {
        var query = new DownloadImageForProductQuery(imageId);

        var result =
            await _mediator.DispatchAsync<DownloadImageForProductQuery, ResultViewModel<Stream>>(query);

        if (!result.IsSuccess || result.Data == null)
            return BadRequest(result.Message);

        return File(result.Data, "image/jpeg");
    }

    [HttpGet("{id:guid}/images")]
    public async Task<IActionResult> DownloadImages(Guid id)
    {
        var query = new DownloadAllImagesForProductQuery(id);

        var result = await _mediator.DispatchAsync<DownloadAllImagesForProductQuery, ResultViewModel<IEnumerable<Stream>>>(query);

        var streams = result.Data;
        if (streams is null) return NotFound();

        var memoryStream = new MemoryStream();

        using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            foreach (var stream in streams)
            {
                var entry = zipArchive.CreateEntry($"{Guid.NewGuid().ToString()}.jpeg");

                using var entryStream = entry.Open();

                stream.CopyTo(entryStream);
            }
        }

        memoryStream.Position = 0;

        var zipFileName = $"images_{id}.zip";

        return File(memoryStream, "application/zip", zipFileName);
    }
}
