using Blazor.Application.IRepository;
using Features.Categories.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Commands;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.Description)
            .MinimumLength(100).WithMessage("{PropertyName} cannot exceed 100 characters")
            .MaximumLength(0).WithMessage("{PropertyName} cannot be less than 1 characters");

        RuleFor(q => q)
            .MustAsync(categoryNameUnique)
            .WithMessage("Category already exists");


        this._categoryRepository = categoryRepository;
    }

    private Task<bool> categoryNameUnique(CreateCategoryCommand command, CancellationToken token)
    {
        return _categoryRepository.IsCategoryUnique(command.Name);
    }
}
