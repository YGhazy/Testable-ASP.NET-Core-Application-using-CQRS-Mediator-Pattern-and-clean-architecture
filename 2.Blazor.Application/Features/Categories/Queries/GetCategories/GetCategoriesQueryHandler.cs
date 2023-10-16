using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Features.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAppLogger<GetCategoriesQueryHandler> _logger;

    public GetCategoriesQueryHandler(IMapper mapper,
        ICategoryRepository categoryRepository,
        IAppLogger<GetCategoriesQueryHandler> logger)
    {
        this._mapper = mapper;
        this._categoryRepository = categoryRepository;
        this._logger = logger;
    }

    public async Task<List<CategoryDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var categories = await _categoryRepository.GetAll();

        // return list of DTO object
        _logger.LogInformation("Category were retrieved successfully");
        return categories.ToList();
    }
}