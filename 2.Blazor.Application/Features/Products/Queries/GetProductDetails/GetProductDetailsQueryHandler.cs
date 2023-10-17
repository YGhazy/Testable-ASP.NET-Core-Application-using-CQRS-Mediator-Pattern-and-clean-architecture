using AutoMapper;
using Blazor.Application.Exceptions;
using Blazor.Application.IRepository;
using MediatR;

namespace Blazor.Application.Features.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDTO>
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public GetProductDetailsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper=mapper;
            this.productRepository=productRepository;
        }
        public async Task<ProductDTO> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {

            var product = await productRepository.Get(request.ID);
            if (product.Id == 0)
                throw new NotFoundException(nameof(product), request.ID);
            return product; 
        }
    }
}
