using Blazor.Application.DTOs;
using Features.Categories.Queries.GetCategories;
using MediatR;

namespace Blazor.Application.Features.Categories.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<List<CategoryDTO>>;