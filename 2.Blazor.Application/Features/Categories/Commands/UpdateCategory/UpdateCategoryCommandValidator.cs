using Blazor.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(CategoryMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");
       
        RuleFor(p => p.Description)
            .MaximumLength(100).WithMessage("{PropertyName} cannot exceed 100 characters")
            .MinimumLength(0).WithMessage("{PropertyName} cannot be less than 1 characters");

        this._categoryRepository = categoryRepository;
    }

    private async Task<bool> CategoryMustExist(int id, CancellationToken arg2)
    {
        var category = await _categoryRepository.Get(id);
        return category != null;
    }
}
