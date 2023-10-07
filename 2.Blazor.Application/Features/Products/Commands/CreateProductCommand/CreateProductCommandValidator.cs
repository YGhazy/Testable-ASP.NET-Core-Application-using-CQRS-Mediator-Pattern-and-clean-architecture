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

    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");
     
        RuleFor(q => q)
     .MustAsync(productUnique)
     .WithMessage("Product already exists");

        this._productRepository=productRepository;
    }

    private async Task<bool> productUnique(CreateProductCommand command, CancellationToken token)
    {
       return await _productRepository.IsProductUnique(command.Name);
    }
}
