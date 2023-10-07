using AutoMapper;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            this.mapper=mapper;
            this.unitOfWork=unitOfWork;
            this.productRepository=productRepository;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator(productRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Product", validationResult);

            var ProductToCreate = mapper.Map<ProductDTO>(request);
            var result= await productRepository.Create(ProductToCreate);

            return result.Id;

        }
    }
}
