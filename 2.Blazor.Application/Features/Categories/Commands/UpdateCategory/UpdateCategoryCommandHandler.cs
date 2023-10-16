using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.Exceptions;
using Blazor.Application.IRepository;
using Blazor.Domain.Entities;
using Features.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Commands.UpdateCategory;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAppLogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, IAppLogger<UpdateCategoryCommandHandler> logger)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        this._logger = logger;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateCategoryCommandValidator(_categoryRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Category), request.Id);
            throw new BadRequestException("Invalid category", validationResult);
        }

        // convert to domain entity object
        var categoryToUpdate = _mapper.Map<CategoryDTO>(request);

        // add to database
        await _categoryRepository.Update(categoryToUpdate);

        // return Unit value
        return Unit.Value;
    }
}