using AutoMapper;
using Blazor.Application.Contracts.Logging;
using Blazor.Application.Exceptions;
using Blazor.Application.Features.Products.Queries;
using Blazor.Application.IRepository;
using Blazor.Domain.Common;
using Blazor.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IAppLogger<CreateProductCommandHandler>  _logger;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, IAppLogger<CreateProductCommandHandler> logger)
        {
            this.mapper=mapper;
            this.productRepository=productRepository;
            _logger=logger;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator(productRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(Product), request.Name);

                throw new BadRequestException("Invalid Product", validationResult);
            }

            var ProductToCreate = mapper.Map<ProductDTO>(request);
            var result= await productRepository.Create(ProductToCreate);

            return result.Id;

        }
    }
}
