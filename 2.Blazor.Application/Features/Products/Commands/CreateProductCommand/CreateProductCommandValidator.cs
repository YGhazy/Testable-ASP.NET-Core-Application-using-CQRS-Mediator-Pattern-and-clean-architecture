using Blazor.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandValidator(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        this._productRepository=productRepository;
        this._categoryRepository=categoryRepository;

        RuleFor(p => p.CategoryId)
                .GreaterThan(0)
                .MustAsync(CategoryMustExist)
                .WithMessage("{PropertyName} does not exist.");


        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(q => q)
     .MustAsync(productUnique)
     .WithMessage("Product already exists");



    }

    private async Task<bool> productUnique(CreateProductCommand command, CancellationToken token)
    {
       return await _productRepository.IsProductUnique(command.Name);
    }

    private async Task<bool> CategoryMustExist(int id, CancellationToken arg2)
    {
        var category = await _categoryRepository.Get(id);
        return category != null;
    }
}
