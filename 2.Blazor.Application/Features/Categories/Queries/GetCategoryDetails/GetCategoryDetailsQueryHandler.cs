using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.DTOs;
using Blazor.Application.Exceptions;
using Blazor.Application.Features.Categories.Queries.GetCategoryDetails;
using Blazor.Application.IRepository;
using Blazor.Domain.Entities;
using Features.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Queries.GetCategories;

public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryDTO>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAppLogger<GetCategoryDetailsQueryHandler> _logger;

    public GetCategoryDetailsQueryHandler(IMapper mapper,
        ICategoryRepository categoryRepository,
        IAppLogger<GetCategoryDetailsQueryHandler> logger)
    {
        this._mapper = mapper;
        this._categoryRepository = categoryRepository;
        this._logger = logger;
    }

    public async Task<CategoryDTO> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var category = await _categoryRepository.Get(request.Id);
        // verify that record exists
        if (category.Id == 0)
            throw new NotFoundException(nameof(Category), request.Id);
        // return list of DTO object
        _logger.LogInformation("Category were retrieved successfully");
        return category;
    }
}