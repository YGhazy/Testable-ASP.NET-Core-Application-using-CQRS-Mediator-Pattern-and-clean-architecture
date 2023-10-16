using Blazor.Application.Exceptions;
using Blazor.Application.IRepository;
using Blazor.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Commands.DeleteCategory;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        // retrieve domain entity object
        var categoryToDelete = await _categoryRepository.Get(request.Id);

        // verify that record exists
        if (categoryToDelete == null)
            throw new NotFoundException(nameof(Category), request.Id);

        // remove from database
        await _categoryRepository.Delete(request.Id);

        // retun record id
        return Unit.Value;
    }
}