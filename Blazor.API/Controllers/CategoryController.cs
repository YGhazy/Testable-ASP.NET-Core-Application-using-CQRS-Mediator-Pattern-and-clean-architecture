using Blazor.Application.Features.Categories.Commands.DeleteCategory;
using Blazor.Application.Features.Categories.Commands.UpdateCategory;
using Blazor.Application.Features.Categories.Queries.GetCategories;
using Blazor.Application.Features.Categories.Queries.GetCategoryDetails;
using Features.Categories.Commands;
using Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<List<CategoryDTO>> Get()
    {
        var categorys = await _mediator.Send(new GetCategoriesQuery());
        return categorys;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _mediator.Send(new GetCategoryDetailsQuery(id));
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateCategoryCommand category)
    {
        var response = await _mediator.Send(category);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<CategorysController>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateCategoryCommand category)
    {
        await _mediator.Send(category);
        return NoContent();
    }

    // DELETE api/<CategorysController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCategoryCommand(id );
        await _mediator.Send(command);
        return NoContent();
    }
}

