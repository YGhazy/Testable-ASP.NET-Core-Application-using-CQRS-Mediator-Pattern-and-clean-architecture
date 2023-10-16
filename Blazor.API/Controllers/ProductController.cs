using Blazor.API.Common;
using Blazor.Application.DTOs;
using Blazor.Application.Features.Products.Commands.CreateProductCommand;
using Blazor.Application.Features.Products.Queries;
using Blazor.Application.Features.Products.Queries.GetProductDetails;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<List<ProductDTO>> Get()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return products;
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _mediator.Send(new GetProductDetailsQuery(id));
        return Ok(product);
    }

    // POST api/<ProductsController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateProductCommand product)
    {
        var response = await _mediator.Send(product);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    //// PUT api/<ProductsController>
    //[HttpPut("{id}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(400)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesDefaultResponseType]
    //public async Task<ActionResult> Put(UpdateProductCommand product)
    //{
    //    await _mediator.Send(product);
    //    return NoContent();
    //}

    //// DELETE api/<ProductsController>/5
    //[HttpDelete("{id}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesDefaultResponseType]
    //public async Task<ActionResult> Delete(int id)
    //{
    //    var command = new DeleteProductCommand { Id = id };
    //    await _mediator.Send(command);
    //    return NoContent();
    //}
}

