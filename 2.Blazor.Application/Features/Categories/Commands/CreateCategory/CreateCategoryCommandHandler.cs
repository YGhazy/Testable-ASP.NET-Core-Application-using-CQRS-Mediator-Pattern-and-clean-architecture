using AutoMapper;
using Blazor.Application.DTOs;
using Blazor.Application.Exceptions;
using Blazor.Application.IRepository;
using Blazor.Domain.Entities;
using Features.Categories.Commands;
using Features.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateCategoryCommandValidator(_categoryRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid category", validationResult);

        // convert to domain entity object
        var categoryToCreate = _mapper.Map<CategoryDTO>(request);

        // add to database
        await _categoryRepository.Create(categoryToCreate);

        // retun record id
        return categoryToCreate.Id;
    }
}